// Nerf

// Original anim idea from Bones' Dance Inducer gun

// Set up defaults for nonexisting vars

// Init handled by server.cs
//if ($Host::Nerf::Enabled $= "")
//	$Host::Nerf::Enabled = "1"; // Enable nerf weapons

if ($Host::Nerf::DanceAnim $= "")
	$Host::Nerf::DanceAnim = "1"; // Enable dance anim for nerf weapons
if ($Host::Nerf::DeathAnim $= "")
	$Host::Nerf::DeathAnim = "0"; // Enable death anim for nerf weapons
if ($Host::Nerf::Prison $= "")
	$Host::Nerf::Prison = "0"; // Enable prison for nerf weapons
if ($Host::Nerf::PrisonTime $= "")
	$Host::Nerf::PrisonTime = "10"; // Time to jail players with nerf weapons

function applyNerf(%targetObject,%sourceObject,%position) {
	if (%targetObject.getClassName() $= "Player") {
		serverPlay3D(blasterExpSound,%position);
		if ($Host::Nerf::DanceAnim == true && !$Host::Nerf::DeathAnim == true)
			%targetObject.setActionThread("Cel" @ getRandom(8)+1,true);
		if ($Host::Nerf::DeathAnim == true)
			%targetObject.setActionThread("Death" @ getRandom(10)+1,true);
		if ($Host::Prison::Enabled == true && $Host::Nerf::Prison == true && $Host::Nerf::PrisonTime > 0
		    && isObject(%targetObject.client) // && isObject(%sourceObject)
		    && !%targetObject.client.isJailed) {
			%emitter = new ParticleEmissionDummy(NerfJailEmitter) {
				position = %position;
				rotation = "1 0 0 0";
				scale = "1 1 1";
				dataBlock = "defaultEmissionDummy";
				emitter = "PlasmaBarrelCrescentEmitter";
				velocity = "1";
			};
			MissionCleanup.add(%emitter); // Well..
			%emitter.schedule(500,"delete");
			jailPlayer(%targetObject.client,false,$Host::Nerf::PrisonTime);
			messageAll('msgClient','\c2%1 put %2 in jail for %3 seconds.',%sourceObject.client.name,%targetObject.client.name,$Host::Nerf::PrisonTime);
		}
	}
}

function nerfEnable () {
	$Host::Nerf::Enabled = 1;

	LightMaleHumanArmor.maxNerfGun = 1;
	LightMaleHumanArmor.maxNerfBallLauncher = 1;
	LightFemaleHumanArmor.maxNerfGun = 1;
	LightFemaleHumanArmor.maxNerfBallLauncher = 1;
	LightMaleBiodermArmor.maxNerfGun = 1;
	LightMaleBiodermArmor.maxNerfBallLauncher = 1;

	MediumMaleHumanArmor.maxNerfGun = 1;
	MediumMaleHumanArmor.maxNerfBallLauncher = 1;
	MediumFemaleHumanArmor.maxNerfGun = 1;
	MediumFemaleHumanArmor.maxNerfBallLauncher = 1;
	MediumMaleBiodermArmor.maxNerfGun = 1;
	MediumMaleBiodermArmor.maxNerfBallLauncher = 1;

	HeavyMaleHumanArmor.maxNerfGun = 1;
	HeavyMaleHumanArmor.maxNerfBallLauncher = 1;
	HeavyFemaleHumanArmor.maxNerfGun = 1;
	HeavyFemaleHumanArmor.maxNerfBallLauncher = 1;
	HeavyMaleBiodermArmor.maxNerfGun = 1;
	HeavyMaleBiodermArmor.maxNerfBallLauncher = 1;

	PureMaleHumanArmor.maxNerfGun = 1;
	PureMaleHumanArmor.maxNerfBallLauncher = 1;
	PureFemaleHumanArmor.maxNerfGun = 1;
	PureFemaleHumanArmor.maxNerfBallLauncher = 1;
	PureMaleBiodermArmor.maxNerfGun = 1;
	PureMaleBiodermArmor.maxNerfBallLauncher = 1;
}

function nerfDisable () {
	$Host::Nerf::Enabled = 0;

	LightMaleHumanArmor.maxNerfGun = 0;
	LightMaleHumanArmor.maxNerfBallLauncher = 0;
	LightFemaleHumanArmor.maxNerfGun = 0;
	LightFemaleHumanArmor.maxNerfBallLauncher = 0;
	LightMaleBiodermArmor.maxNerfGun = 0;
	LightMaleBiodermArmor.maxNerfBallLauncher = 0;

	MediumMaleHumanArmor.maxNerfGun = 0;
	MediumMaleHumanArmor.maxNerfBallLauncher = 0;
	MediumFemaleHumanArmor.maxNerfGun = 0;
	MediumFemaleHumanArmor.maxNerfBallLauncher = 0;
	MediumMaleBiodermArmor.maxNerfGun = 0;
	MediumMaleBiodermArmor.maxNerfBallLauncher = 0;

	HeavyMaleHumanArmor.maxNerfGun = 0;
	HeavyMaleHumanArmor.maxNerfBallLauncher = 0;
	HeavyFemaleHumanArmor.maxNerfGun = 0;
	HeavyFemaleHumanArmor.maxNerfBallLauncher = 0;
	HeavyMaleBiodermArmor.maxNerfGun = 0;
	HeavyMaleBiodermArmor.maxNerfBallLauncher = 0;

	PureMaleHumanArmor.maxNerfGun = 0;
	PureMaleHumanArmor.maxNerfBallLauncher = 0;
	PureFemaleHumanArmor.maxNerfGun = 0;
	PureFemaleHumanArmor.maxNerfBallLauncher = 0;
	PureMaleBiodermArmor.maxNerfGun = 0;
	PureMaleBiodermArmor.maxNerfBallLauncher = 0;

	%count = ClientGroup.getCount();
	for (%i=0;%i<%count;%i++) {
		%client = ClientGroup.getObject(%i);
		%player = %client.player;      
		if (isObject(%player)) {
			%player.setInventory(NerfGun,0,true);
			%client.setWeaponsHudItem(NerfGun,0,0);
			%player.setInventory(NerfBallLauncher,0,true);
			%player.setInventory(NerfBallLauncherAmmo,0,true);
			%client.setWeaponsHudItem(NerfBallLauncher,0,0);
		}
	}
}
