// These have been secured against all those wanna-be-hackers.
$VoteMessage["VoteAdminPlayer"] = "Admin Player";
$VoteMessage["VoteKickPlayer"] = "Kick Player";
$VoteMessage["BanPlayer"] = "Ban Player";
$VoteMessage["VoteChangeMission"] = "change the mission to";
$VoteMessage["VoteTeamDamage", 0] = "enable team damage";
$VoteMessage["VoteTeamDamage", 1] = "disable team damage";
$VoteMessage["VoteTournamentMode"] = "change the server to";
$VoteMessage["VoteFFAMode"] = "change the server to";
$VoteMessage["VoteChangeTimeLimit"] = "change the time limit to";
$VoteMessage["VoteMatchStart"] = "start the match";
$VoteMessage["VoteGreedMode", 0] = "enable Hoard Mode";
$VoteMessage["VoteGreedMode", 1] = "disable Hoard Mode";
$VoteMessage["VoteHoardMode", 0] = "enable Greed Mode";
$VoteMessage["VoteHoardMode", 1] = "disable Greed Mode";
// JTL
$VoteMessage["VotePurebuild", 0] = "enable pure building";
$VoteMessage["VotePurebuild", 1] = "disable pure building";
$VoteMessage["VoteCascadeMode", 0] = "enable cascade mode";
$VoteMessage["VoteCascadeMode", 1] = "disable cascade mode";
$VoteMessage["VoteExpertMode", 0] = "enable expert mode";
$VoteMessage["VoteExpertMode", 1] = "disable expert mode";
$VoteMessage["VoteVehicles", 0] = "enable vehicles";
$VoteMessage["VoteVehicles", 1] = "disable vehicles";
$VoteMessage["VoteSatchelCharge", 0] = "enable satchel charges";
$VoteMessage["VoteSatchelCharge", 1] = "disable satchel charges";
$VoteMessage["VoteOnlyOwnerDeconstruct", 0] = "enable only owner deconstruct";
$VoteMessage["VoteOnlyOwnerDeconstruct", 1] = "disable only owner deconstruct";
$VoteMessage["VoteOnlyOwnerCascade", 0] = "enable only owner cascade";
$VoteMessage["VoteOnlyOwnerCascade", 1] = "disable only owner cascade";
$VoteMessage["VoteOnlyOwnerRotate", 0] = "enable only owner rotate";
$VoteMessage["VoteOnlyOwnerRotate", 1] = "disable only owner rotate";
$VoteMessage["VoteOnlyOwnerCubicReplace", 0] = "enable only owner cubic-replace";
$VoteMessage["VoteOnlyOwnerCubicReplace", 1] = "disable only owner cubic-replace";
$VoteMessage["VoteInvincibleArmors", 0] = "enable invincible armors";
$VoteMessage["VoteInvincibleArmors", 1] = "disable invincible armors";
$VoteMessage["VoteInvincibleDeployables", 0] = "enable invincible deployables";
$VoteMessage["VoteInvincibleDeployables", 1] = "disable invincible deployables";
$VoteMessage["VoteUndergroundMode", 0] = "enable underground mode";
$VoteMessage["VoteUndergroundMode", 1] = "disable underground mode";
$VoteMessage["VoteHazardMode", 0] = "enable hazard mode";
$VoteMessage["VoteHazardMode", 1] = "disable hazard mode";
$VoteMessage["VoteMTCMode", 0] = "enable MTC mode";
$VoteMessage["VoteMTCMode", 1] = "disable MTC mode";
$VoteMessage["VoteRemoveDeployables"] = "remove all deployables in mission";
$VoteMessage["VoteGlobalPowerCheck"] = "remove all duplicate deployables";
$VoteMessage["VoteRemoveDupDeployables"] = "remove all duplicate deployables";
$VoteMessage["VoteRemoveNonPoweredDeployables"] = "remove all deployables without power";
$VoteMessage["VoteRemoveOrphanedDeployables"] = "remove all orphaned deployables";
$VoteMessage["VotePrison", 0] = "enable prison";
$VoteMessage["VotePrison", 1] = "disable prison";
$VoteMessage["VotePrisonKilling", 0] = "enable jailing killers";
$VoteMessage["VotePrisonKilling", 1] = "disable jailing killers";
$VoteMessage["VotePrisonTeamKilling", 0] = "enable jailing team killers";
$VoteMessage["VotePrisonTeamKilling", 1] = "disable jailing team killers";
$VoteMessage["VotePrisonDeploySpam", 0] = "enable jailing deploy spammers";
$VoteMessage["VotePrisonDeploySpam", 1] = "disable jailing deploy spammers";
$VoteMessage["VoteNerfWeapons", 0] = "enable nerf weapons";
$VoteMessage["VoteNerfWeapons", 1] = "disable nerf weapons";
$VoteMessage["VoteNerfDance", 0] = "enable nerf dance mode";
$VoteMessage["VoteNerfDance", 1] = "disable nerf dance mode";
$VoteMessage["VotenerfDeath", 0] = "enable nerf death mode";
$VoteMessage["VotenerfDeath", 1] = "disable nerf death mode";
$VoteMessage["VoteNerfPrison", 0] = "enable nerf prison mode";
$VoteMessage["VoteNerfPrison", 1] = "disable nerf prison mode";
// End JTL

function serverCmdStartNewVote(%client, %typeName, %arg1, %arg2, %arg3, %arg4, %playerVote)
{
   //DEMO VERSION - only voteKickPlayer is allowed
   if ((isDemo()) && %typeName !$= "VoteKickPlayer")
   {
      messageClient(%client, '', "All voting options except to kick a player are disabled in the DEMO VERSION.");
      return;
   }

   // haha - who gets the last laugh... No admin for you!
   if( %typeName $= "VoteAdminPlayer" && (!$Host::allowAdminPlayerVotes && !%client.isSuperAdmin))
      return;

   %typePass = true;

   // if not a valid vote, turn back.
   if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteTeamDamage" )
      if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteHoardMode" )
         if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteGreedMode" )
// JTL
            if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VotePurebuild" )
              if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteCascadeMode" )
                if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteExpertMode" )
                  if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteVehicles" )
                    if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteSatchelCharge" )
                      if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteOnlyOwnerDeconstruct" )
                        if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteOnlyOwnerCascade" )
                          if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteOnlyOwnerRotate" )
                            if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteOnlyOwnerCubicReplace" )
                              if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteRemoveDeployables" )
                                if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteGlobalPowerCheck" )
                                  if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteRemoveDupDeployables" )
                                    if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteRemoveNonPoweredDeployables" )
                                      if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteRemoveOrphanedDeployables" )
                                        if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteInvincibleArmors" )
                                          if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteInvincibleDeployables" )
                                            if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteUndergroundMode" )
                                              if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteHazardMode" )
                                                if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteMTCMode" )
                                                  if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VotePrison" )
                                                    if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VotePrisonKilling" )
                                                      if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VotePrisonTeamKilling" )
                                                        if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VotePrisonDeploySpam" )
                                                          if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteNerfWeapons" )
                                                            if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteNerfDance" )
                                                              if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteNerfDeath" )
                                                                if( $VoteMessage[ %typeName ] $= "" && %typeName !$= "VoteNerfPrison" )
                                                                  %typePass = false;
// End JTL

   if(( $VoteMessage[ %typeName, $TeamDamage ] $= "" && %typeName $= "VoteTeamDamage" ))
      %typePass = false;
// JTL
   if(( $VoteMessage[ %typeName, $Host::Purebuild ] $= "" && %typeName $= "VotePurebuild" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::Cascade ] $= "" && %typeName $= "VoteCascadeMode" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::ExpertMode ] $= "" && %typeName $= "VoteExpertMode" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::Vehicles ] $= "" && %typeName $= "VoteVehicles" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::SatchelChargeEnabled ] $= "" && %typeName $= "VoteSatchelCharge" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::OnlyOwnerDeconstruct ] $= "" && %typeName $= "VoteOnlyOwnerDeconstruct" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::OnlyOwnerCascade ] $= "" && %typeName $= "VoteOnlyOwnerCascade" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::OnlyOwnerRotate ] $= "" && %typeName $= "VoteOnlyOwnerRotate" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::OnlyOwnerCubicReplace ] $= "" && %typeName $= "VoteOnlyOwnerCubicReplace" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::InvincibleArmors ] $= "" && %typeName $= "VoteInvincibleArmors" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::InvincibleDeployables ] $= "" && %typeName $= "VoteInvincibleDeployables" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::AllowUnderground ] $= "" && %typeName $= "VoteUndergroundMode" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::Hazard::Enabled ] $= "" && %typeName $= "VoteHazardMode" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::MTC::Enabled ] $= "" && %typeName $= "VoteMTCMode" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::Prison::Enabled ] $= "" && %typeName $= "VotePrison" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::Prison::Kill ] $= "" && %typeName $= "VotePrisonKilling" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::Prison::TeamKill ] $= "" && %typeName $= "VotePrisonTeamKilling" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::Prison::DeploySpam ] $= "" && %typeName $= "VotePrisonDeploySpam" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::Nerf::Enabled ] $= "" && %typeName $= "VoteNerfWeapons" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::Nerf::DanceAnim ] $= "" && %typeName $= "VoteNerfDance" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::Nerf::DeathAnim ] $= "" && %typeName $= "VoteNerfDeath" ))
      %typePass = false;
   if(( $VoteMessage[ %typeName, $Host::Nerf::Prison ] $= "" && %typeName $= "VoteNerfPrison" ))
      %typePass = false;
// End JTL

   if( !%typePass )
      return; // -> bye ;)

   // z0dd - ZOD, 10/03/02. This was busted, BanPlayer was never delt with.
   if( %typeName $= "BanPlayer" )
   {
      if( !%client.isSuperAdmin || %arg1.isAdmin )
      {
         return; // -> bye ;)
      }
      else
      {
         ban( %arg1, %client );
         return;
      }
   }

   %isAdmin = ( %client.isAdmin || %client.isSuperAdmin );

// JTL
   if(%typeName $= "VoteVehicles" && !%isAdmin)
     if(%typeName $= "VoteVehicles" && !%isAdmin)
       if(%typeName $= "VoteSatchelCharge" && !%isAdmin)
         if(%typeName $= "VoteOnlyOwnerDeconstruct" && !%isAdmin)
           if(%typeName $= "VoteOnlyOwnerCascade" && !%isAdmin)
             if(%typeName $= "VoteOnlyOwnerRotate" && !%isAdmin)
               if(%typeName $= "VoteOnlyOwnerCubicReplace" && !%isAdmin)
                 if(%typeName $= "VoteRemoveDeployables" && !%isAdmin)
                   if(%typeName $= "VoteGlobalPowerCheck" && !%isAdmin)
                     if(%typeName $= "VoteRemoveDupDeployables" && !%isAdmin)
                       if(%typeName $= "VoteRemoveNonPoweredDeployables" && !%isAdmin)
                         if(%typeName $= "VoteRemoveOrphanedDeployables" && !%isAdmin)
                           if(%typeName $= "VoteInvincibleArmors" && !%isAdmin)
                             if(%typeName $= "VoteInvincibleDeployables" && !%isAdmin)
                               if(%typeName $= "VoteUndergroundMode" && !%isAdmin)
                                 if(%typeName $= "VoteHazardMode" && !%isAdmin)
                                   if(%typeName $= "VoteMTCMode" && !%isAdmin)
                                     if(%typeName $= "VotePrison" && !%isAdmin)
                                       if(%typeName $= "VotePrisonKilling" && !%isAdmin)
                                         if(%typeName $= "VotePrisonTeamKilling" && !%isAdmin)
                                           if(%typeName $= "VotePrisonDeploySpam" && !%isAdmin)
                                             if(%typeName $= "VoteNerfWeapons" && !%isAdmin)
                                               if(%typeName $= "VoteNerfDance" && !%isAdmin)
                                                 if(%typeName $= "VoteNerfDeath" && !%isAdmin)
                                                   if(%typeName $= "VoteNerfPrison" && !%isAdmin)
                                                     %typePass = false;

   if( !%typePass )
      return; // -> bye ;)
// End JTL

   // keep these under the server's control. I win.
   if( !%playerVote )
      %actionMsg = $VoteMessage[ %typeName ];
   else if( %typeName $= "VoteTeamDamage" || %typeName $= "VoteGreedMode" || %typeName $= "VoteHoardMode" )
      %actionMsg = $VoteMessage[ %typeName, $TeamDamage ];
// JTL
   else if( %typeName $= "VotePurebuild"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::Purebuild ];
   else if( %typeName $= "VoteCascadeMode"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::Cascade ];
   else if( %typeName $= "VoteExpertMode"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::ExpertMode ];
   else if( %typeName $= "VoteVehicles"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::Vehicles ];
   else if( %typeName $= "VoteSatchelCharge"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::SatchelChargeEnabled ];
   else if( %typeName $= "VoteOnlyOwnerDeconstruct"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::OnlyOwnerDeconstruct ];
   else if( %typeName $= "VoteOnlyOwnerCascade"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::OnlyOwnerCascade ];
   else if( %typeName $= "VoteOnlyOwnerRotate"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::OnlyOwnerRotate ];
   else if( %typeName $= "VoteOnlyOwnerCubicReplace"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::OnlyOwnerCubicReplace ];
   else if( %typeName $= "VoteInvincibleArmors"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::InvincibleArmors ];
   else if( %typeName $= "VoteInvincibleDeployables"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::InvincibleDeployables ];
   else if( %typeName $= "VoteUndergroundMode"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::AllowUnderground ];
   else if( %typeName $= "VoteHazardMode"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::Hazard::Enabled ];
   else if( %typeName $= "VoteMTCMode"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::MTC::Enabled ];
   else if( %typeName $= "VotePrison"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::Prison::Enabled ];
   else if( %typeName $= "VotePrisonKilling"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::Prison::Kill ];
   else if( %typeName $= "VotePrisonTeamKilling"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::Prison::TeamKill ];
   else if( %typeName $= "VotePrisonDeploySpam"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::Prison::DeploySpam ];
   else if( %typeName $= "VoteNerfWeapons"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::Nerf::Enabled ];
   else if( %typeName $= "VoteNerfDance"  )
      %actionMsg = $VoteMessage[ %typeName, ($Host::Nerf::DanceAnim && !$Host::Nerf::DeathAnim) ];
   else if( %typeName $= "VoteNerfDeath"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::Nerf::DeathAnim ];
   else if( %typeName $= "VoteNerfPrison"  )
      %actionMsg = $VoteMessage[ %typeName, $Host::Nerf::Prison ];
// End JTL
   else
      %actionMsg = $VoteMessage[ %typeName ];

   if( !%client.canVote && !%isAdmin )
      return;

   if ( ( !%isAdmin || ( %arg1.isAdmin && ( %client != %arg1 ) ) ) &&     // z0dd - ToS 4/2/02: Allow SuperAdmins to kick Admins
        !( ( %typeName $= "VoteKickPlayer" ) && %client.isSuperAdmin ) )  // z0dd - ToS 4/2/02: Allow SuperAdmins to kick Admins
   {
      %teamSpecific = false;
           %gender = (%client.sex $= "Male" ? 'he' : 'she');
      if ( Game.scheduleVote $= "" )
      {
         %clientsVoting = 0;

                        //send a message to everyone about the vote...
         if ( %playerVote )
              {
            %teamSpecific = ( %typeName $= "VoteKickPlayer" ) && ( Game.numTeams > 1 );
            %kickerIsObs = %client.team == 0;
            %kickeeIsObs = %arg1.team == 0;
            %sameTeam = %client.team == %arg1.team;

            if( %kickeeIsObs )
            {
               %teamSpecific = false;
               %sameTeam = false;
            }

            if(( !%sameTeam && %teamSpecific) && %typeName !$= "VoteAdminPlayer")
            {
               messageClient(%client, '', "\c2Player votes must be team based.");
               return;
            }

            // kicking is team specific
            if( %typeName $= "VoteKickPlayer" )
            {
               if(%arg1.isSuperAdmin)
               {
                  messageClient(%client, '', '\c2You can not %1 %2, %3 is a Super Admin!', %actionMsg, %arg1.name, %gender);
                  return;
               }

               Game.kickClient = %arg1;
               Game.kickClientName = %arg1.name;
               Game.kickGuid = %arg1.guid;
               Game.kickTeam = %arg1.team;

               if(%teamSpecific)
               {
                  for ( %idx = 0; %idx < ClientGroup.getCount(); %idx++ )
                  {
                     %cl = ClientGroup.getObject( %idx );

                     if (%cl.team == %client.team && !%cl.isAIControlled())
                     {
                        messageClient( %cl, 'VoteStarted', '\c2%1 initiated a vote to %2 %3.', %client.name, %actionMsg, %arg1.name);
                        %clientsVoting++;
                     }
                  }
               }
               else
               {
                  for ( %idx = 0; %idx < ClientGroup.getCount(); %idx++ )
                  {
                     %cl = ClientGroup.getObject( %idx );
                     if ( !%cl.isAIControlled() )
                     {
                        messageClient( %cl, 'VoteStarted', '\c2%1 initiated a vote to %2 %3.', %client.name, %actionMsg, %arg1.name);
                        %clientsVoting++;
                     }
                  }
               }
            }
            else
            {
               for ( %idx = 0; %idx < ClientGroup.getCount(); %idx++ )
               {
                  %cl = ClientGroup.getObject( %idx );
                  if ( !%cl.isAIControlled() )
                  {
                     messageClient( %cl, 'VoteStarted', '\c2%1 initiated a vote to %2 %3.', %client.name, %actionMsg, %arg1.name);
                     %clientsVoting++;
                  }
               }
            }
         }
         else if ( %typeName $= "VoteChangeMission" )
         {
            for ( %idx = 0; %idx < ClientGroup.getCount(); %idx++ )
            {
               %cl = ClientGroup.getObject( %idx );
               if ( !%cl.isAIControlled() )
               {
                  messageClient( %cl, 'VoteStarted', '\c2%1 initiated a vote to %2 %3 (%4).', %client.name, %actionMsg, %arg1, %arg2 );
                  %clientsVoting++;
               }
            }
         }
         else if (%arg1 !$= 0)
         {
                                if (%arg2 !$= 0)
                      {
               if(%typeName $= "VoteTournamentMode")
               {
                  %admin = getAdmin();
                  if(%admin > 0)
                  {
                     for ( %idx = 0; %idx < ClientGroup.getCount(); %idx++ )
                     {
                        %cl = ClientGroup.getObject( %idx );
                        if ( !%cl.isAIControlled() )
                        {
                           messageClient( %cl, 'VoteStarted', '\c2%1 initiated a vote to %2 Tournament Mode (%3).', %client.name, %actionMsg, %arg1);
                           %clientsVoting++;
                        }
                     }
                  }
                  else
                  {
                     messageClient( %client, 'clientMsg', 'There must be a server admin to play in Tournament Mode.');
                     return;
                  }
               }
               else
               {
                  for ( %idx = 0; %idx < ClientGroup.getCount(); %idx++ )
                  {
                     %cl = ClientGroup.getObject( %idx );
                     if ( !%cl.isAIControlled() )
                     {
                        messageClient( %cl, 'VoteStarted', '\c2%1 initiated a vote to %2 %3 %4.', %client.name, %actionMsg, %arg1, %arg2);
                        %clientsVoting++;
                     }
                  }
                                   }
            }
            else
            {
               for ( %idx = 0; %idx < ClientGroup.getCount(); %idx++ )
               {
                  %cl = ClientGroup.getObject( %idx );
                  if ( !%cl.isAIControlled() )
                  {
                               messageClient( %cl, 'VoteStarted', '\c2%1 initiated a vote to %2 %3.', %client.name, %actionMsg, %arg1);
                     %clientsVoting++;
                  }
               }
            }
         }
                        else
         {
            for ( %idx = 0; %idx < ClientGroup.getCount(); %idx++ )
            {
               %cl = ClientGroup.getObject( %idx );
               if ( !%cl.isAIControlled() )
               {
                       messageClient( %cl, 'VoteStarted', '\c2%1 initiated a vote to %2.', %client.name, %actionMsg);
                  %clientsVoting++;
               }
            }
         }

         // open the vote hud for all clients that will participate in this vote
         if(%teamSpecific)
         {
            for ( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
            {
               %cl = ClientGroup.getObject( %clientIndex );

               if(%cl.team == %client.team && !%cl.isAIControlled())
                  messageClient(%cl, 'openVoteHud', "", %clientsVoting, ($Host::VotePassPercent / 100));
            }
         }
         else
         {
            for ( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
            {
               %cl = ClientGroup.getObject( %clientIndex );
               if ( !%cl.isAIControlled() )
                  messageClient(%cl, 'openVoteHud', "", %clientsVoting, ($Host::VotePassPercent / 100));
            }
         }

         clearVotes();
         Game.voteType = %typeName;
         Game.scheduleVote = schedule( ($Host::VoteTime * 1000), 0, "calcVotes", %typeName, %arg1, %arg2, %arg3, %arg4 );
         %client.vote = true;
         messageAll('addYesVote', "");

         if(!%client.team == 0)
            clearBottomPrint(%client);
      }
      else
         messageClient( %client, 'voteAlreadyRunning', '\c2A vote is already in progress.' );
   }
   else
   {
      if ( Game.scheduleVote !$= "" && Game.voteType $= %typeName )
      {
         messageAll('closeVoteHud', "");
         cancel(Game.scheduleVote);
         Game.scheduleVote = "";
      }

      // if this is a superAdmin, don't kick or ban
      if(%arg1 != %client)
      {
         if(!%arg1.isSuperAdmin)
         {
            // Set up kick/ban values:
            if ( %typeName $= "VoteBanPlayer" || %typeName $= "VoteKickPlayer" )
            {
               Game.kickClient = %arg1;
               Game.kickClientName = %arg1.name;
               Game.kickGuid = %arg1.guid;
               Game.kickTeam = %arg1.team;
            }

            //Tinman - PURE servers can't call "eval"
            //Mark - True, but neither SHOULD a normal server
            //     - thanks Ian Hardingham
            Game.evalVote(%typeName, true, %arg1, %arg2, %arg3, %arg4);
         }
         else
            messageClient(%client, '', '\c2You can not %1 %2, %3 is a Super Admin!', %actionMsg, %arg1.name, %gender);
      }
   }

   %client.canVote = false;
   %client.rescheduleVote = schedule( ($Host::voteSpread * 1000) + ($Host::voteTime * 1000) , 0, "resetVotePrivs", %client );
}

function resetVotePrivs( %client )
{
   //messageClient( %client, '', 'You may now start a new vote.');
   %client.canVote = true;
   %client.rescheduleVote = "";
}

function serverCmdSetPlayerVote(%client, %vote)
{
   // players can only vote once
   if( %client.vote $= "" )
   {
      %client.vote = %vote;
      if(%client.vote == 1)
         messageAll('addYesVote', "");
      else
         messageAll('addNoVote', "");

      commandToClient(%client, 'voteSubmitted', %vote);
   }
}

function calcVotes(%typeName, %arg1, %arg2, %arg3, %arg4)
{
   if(%typeName $= "voteMatchStart")
      if($MatchStarted || $countdownStarted)
         return;

   for ( %clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++ )
   {
      %cl = ClientGroup.getObject( %clientIndex );
      messageClient(%cl, 'closeVoteHud', "");

      if ( %cl.vote !$= "" )
      {
         if ( %cl.vote )
         {
            Game.votesFor[%cl.team]++;
            Game.totalVotesFor++;
         }
         else
         {
            Game.votesAgainst[%cl.team]++;
            Game.totalVotesAgainst++;
         }
      }
      else
      {
         Game.votesNone[%cl.team]++;
         Game.totalVotesNone++;
      }
   }
   //Tinman - PURE servers can't call "eval"
   //Mark - True, but neither SHOULD a normal server
   //     - thanks Ian Hardingham
   Game.evalVote(%typeName, false, %arg1, %arg2, %arg3, %arg4);
   Game.scheduleVote = "";
   Game.kickClient = "";
   clearVotes();
}

function clearVotes()
{
   for(%clientIndex = 0; %clientIndex < ClientGroup.getCount(); %clientIndex++)
   {
      %client = ClientGroup.getObject(%clientIndex);
      %client.vote = "";
      messageClient(%client, 'clearVoteHud', "");
   }

   for(%team = 1; %team < 5; %team++)
   {
      Game.votesFor[%team] = 0;
      Game.votesAgainst[%team] = 0;
      Game.votesNone[%team] = 0;
      Game.totalVotesFor = 0;
      Game.totalVotesAgainst = 0;
      Game.totalVotesNone = 0;
   }
}

// Tournament mode Stuff-----------------------------------

function setModeFFA( %mission, %missionType )
{
   if( $Host::TournamentMode )
   {
      $Host::TournamentMode = false;

      if( isObject( Game ) )
         Game.gameOver();

      loadMission(%mission, %missionType, false);
   }
}

//------------------------------------------------------------------

function setModeTournament( %mission, %missionType )
{
   if( !$Host::TournamentMode )
   {
      $Host::TournamentMode = true;

      if( isObject( Game ) )
         Game.gameOver();

      loadMission(%mission, %missionType, false);
   }
}
