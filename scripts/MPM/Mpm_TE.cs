if ($mpm_TE != 1)
   {
   $mpm_load[$mpm_loads] = Mpm_TE_Load;
   $mpm_loads++;
   $mpm_TE = 1;
   }


datablock ItemData(Mpm_TE_Load) 
{
   cost = 10;
   missile = Mpm_B_MIS;
   name = "MPB Telleporter";
   friendly = 1;
   slot = 0;
};



datablock ParticleData(Mpm_TE_P)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
   lifetimeMS           = 1500;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 30.0;
   spinRandomMax = 30.0;
   windcoefficient = 0;
   textureName          = "skins/jetflare03";

   colors[0]     = "0.3 0.3 1.0 0.1";
   colors[1]     = "0.3 0.3 1.0 1";
   colors[2]     = "0.3 0.3 1.0 1";
   colors[3]     = "0.3 0.3 1.0 0.1";

   sizes[0]      = 5;
   sizes[1]      = 5;
   sizes[2]      = 5;
   sizes[3]      = 5;

   times[0]      = 0.25;
   times[1]      = 0.5;
   times[2]      = 0.75;
   times[3]      = 1;

};

datablock ParticleEmitterData(Mpm_TE_PE)
{
   lifetimeMS        = 10;
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 0.01;
   velocityVariance = 0.0;
   ejectionoffset = 8;
   thetaMin         = 80.0;
   thetaMax         = 100.0;
	
   phiReferenceVel = "180";
   phiVariance = "5";
   orientParticles  = false;
   orientOnVelocity = false;

   particles = "Mpm_TE_P";
};


function Mpm_TE_Load::Explode(%data,%p,%pos)
{
//echo("explode");
if (IsObject(%p))
	{
        //if(VectorDist(%p.targetlocation,%pos)<50)
        %p.load.AtTarget(%p);
	}
}

function Mpm_TE_Load::AtTarget(%data,%p)
{
//echo("attar");
if (IsObject(%p))
	{
        %p.source.mpb.MPM_TelleportMpb(VectorAdd(%p.getTransform(),"0 0 10"));
	}
}


function Mpb_undeploy(%obj)
{
   if (%obj.deploySchedule)
   {
      %obj.deploySchedule.clear();
      %obj.deploySchedule = "";
   }

   if (%obj.deployed !$= "" && %obj.deployed == 1)
   {
      %obj.setThreadDir($DeployThread, false);
      %obj.playThread($DeployThread,"deploy");
      %obj.playAudio($DeploySound, MobileBaseUndeploySound);
      %obj.station.setThreadDir($DeployThread, false);
      %obj.station.getDataBlock().onLosePowerDisabled(%obj.station);
      %obj.station.clearSelfPowered();
      %obj.station.goingOut=false;
      %obj.station.notDeployed = 1;
      %obj.station.playAudio($DeploySound, MobileBaseStationUndeploySound);

      if (isObject(%obj.turret) && isObject(%turretClient = %obj.turret.getControllingClient()) !$= "")
      {
         CommandToServer( 'resetControlObject', %turretClient );
      }
      if (isObject(%obj.turret))
          %obj.turret.setThreadDir($DeployThread, false);
       //[most]
      if (isObject(%obj.nuke))
	      %obj.nuke.mpm_all_off(0);
       //[most]
      %obj.turret.clearTarget();
      %obj.turret.setTargetObject(-1);

      %obj.turret.playAudio($DeploySound, MobileBaseTurretUndeploySound);
      %obj.shield.open();
      %obj.shield.schedule(1000,"delete");
      %obj.deploySchedule = "";

      %obj.fullyDeployed = 0;

      %obj.noEnemyControl = 0;
   }
   %obj.deployed = 0;
  
}

function GameBase::MPM_TelleportMpb(%mpb,%target)
{
%p1 = CreateEmitter(%mpb.getTransform(),Mpm_TE_PE);
%p2 = CreateEmitter(%mpb.getTransform(),Mpm_TE_PE);
%p2.setRotation("1 0 0 3.14");
%p3 = CreateEmitter(%target,Mpm_TE_PE);
%p4 = CreateEmitter(%target,Mpm_TE_PE);
%p4.setRotation("1 0 0 3.14");
if (%mpb.deployed)
   {
   if (IsObject(%mpb.nuke.leftpad.getMountedObject(0)))
	    %mpb.mountObject(%mpb.nuke.leftpad.getMountedObject(0),0);
   else if (IsObject(%mpb.nuke.rightpad.getMountedObject(0)))
   	    %mpb.mountObject(%mpb.nuke.rightpad.getMountedObject(0),0);

   %mpb.schedule(7000,"telleport",%target);
   Schedule(7000,%mpb,"RadiusTelleport",%mpb,%mpb.getTransform(),%target);
   %p1.schedule(11000,"delete");
   %p2.schedule(11000,"delete");
   %p3.schedule(11000,"delete");
   %p4.schedule(11000,"delete");
   mpb_undeploy(%mpb);
  }
else
    {
    %mpb.schedule(1500,"telleport",%target);
    Schedule(1500,%mpb,"RadiusTelleport",%mpb,%mpb.getTransform(),%target);
    %p1.schedule(5500,"delete");
    %p2.schedule(5500,"delete");
    %p3.schedule(5500,"delete");
    %p4.schedule(5500,"delete");
    }

}

function RadiusTelleport(%obj,%pos,%target)
{
%mask = $TypeMasks::PlayerObjectType | $TypeMasks::ItemObjectType;
InitContainerRadiusSearch(%pos, 8, %mask);
while ((%test = containerSearchNext()) != 0) 
{
if (%test != %obj && %test != %obj.getMountedObject(0))
   {
   %offset = VectorSub(%test.getTransform(),%pos);
   %test.telleport(VectorAdd(%target,%offset));
   }
}
}

function GameBase::telleport(%obj,%pos)
{
teleportStartFX(%obj);
schedule(500,0,"teleportEndFX",%obj);
%obj.schedule(500,"SetTransform",%pos SPC rot(%obj));
}

