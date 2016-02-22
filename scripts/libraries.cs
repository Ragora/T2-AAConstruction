//Contain basic data libraries.
//Soon to be updated with more usefull stuff.

// NOTE - any changes here must be considered in expertlibraries.cs !!!

//** New format of information ** 

$packSettings["spine"] = 7;
$packSetting["spine",0] = "0.5 0.5 1.5 1.5 meters in height";
$packSetting["spine",1] = "0.5 0.5 4 4 meters in height";
$packSetting["spine",2] = "0.5 0.5 8 8 meters in height";
$packSetting["spine",3] = "0.5 0.5 40 40 meters in height";
$packSetting["spine",4] = "0.5 0.5 160 160 meters in height";
$packSetting["spine",5] = "0.5 6 160 auto adjusting";
$packSetting["spine",6] = "0.5 8 160 pad";
$packSetting["spine",7] = "0.5 8 160 wooden pad";

$packSettings["mspine"] = 7;
$packSetting["mspine",0] = "1 1 1 2 2 0.5 1 meters in height";
$packSetting["mspine",1] = "1 1 4 2 2 0.5 4 meters in height";
$packSetting["mspine",2] = "1 1 8 2 2 0.5 8 meters in height";
$packSetting["mspine",3] = "1 1 40 2 2 0.5 40 meters in height";
$packSetting["mspine",4] = "1 1 160 2 2 0.5 160 meters in height";
$packSetting["mspine",5] = "1 8 160 2 2 0.5 auto adjusting";
$packSetting["mspine",6] = "1 6 160 2 2 0.5 normal rings";
$packSetting["mspine",7] = "1 8 160 8 8 0.5 platform rings";

$expertSettings["mspine"] = 1;
$expertSetting["mspine",0] = "Rings disabled";
$expertSetting["mspine",1] = "Rings enabled";

$packSettings["floor"] = 5;
$packSetting["floor",0] = "10 10 20 10 10 10 10 meters wide";
$packSetting["floor",1] = "20 20 20 20 20 20 20 meters wide";
$packSetting["floor",2] = "30 30 20 30 30 30 30 meters wide";
$packSetting["floor",3] = "40 40 20 40 40 40 40 meters wide";
$packSetting["floor",4] = "50 50 20 50 50 50 50 meters wide";
$packSetting["floor",5] = "60 60 20 60 60 60 60 meters wide";

$expertSettings["floor"] = 4;
$expertSetting["floor",0] = "1.5 meters in height";
$expertSetting["floor",1] = "5 meters in height";
$expertSetting["floor",2] = "10 meters in height";
$expertSetting["floor",3] = "20 meters in height";
$expertSetting["floor",4] = "40 meters in height";

$packSettings["walk"] = 12;
$packSetting["walk",0] = "0 flat";
$packSetting["walk",1] = "5 Sloped 5 degrees up";
$packSetting["walk",2] = "10 Sloped 10 degrees up";
$packSetting["walk",3] = "20 Sloped 20 degrees up";
$packSetting["walk",4] = "45 Sloped 45 degrees up";
$packSetting["walk",5] = "60 Sloped 60 degrees up";
$packSetting["walk",6] = "90 Sloped 90 degrees up";
$packSetting["walk",7] = "-5 Sloped 5 degrees down";
$packSetting["walk",8] = "-10 Sloped 10 degrees down";
$packSetting["walk",9] = "-20 Sloped 20 degrees down";
$packSetting["walk",10] = "-45 Sloped 45 degrees down";
$packSetting["walk",11] = "-60 Sloped 60 degrees down";
$packSetting["walk",12] = "-90 Sloped 90 degrees down";

$expertSettings["walk"] = 3;
$expertSetting["walk",0] = "Normal walkway";
$expertSetting["walk",1] = "No-flicker walkway";
$expertSetting["walk",2] = "Double width walkway";
$expertSetting["walk",3] = "Double height walkway";

$packSettings["blast"] = 3;
$packSetting["blast",0] = "deploy from inside";
$packSetting["blast",1] = "deploy in frame";
$packSetting["blast",2] = "deploy from outside";
$packSetting["blast",3] = "deploy full thickness";

$expertSettings["blast"] = 1;
$expertSetting["blast",0] = "Normal Blast Wall";
$expertSetting["blast",1] = "Multiple Blast Walls";

$packSettings["forcefield"] = 20;
$packSetting["forcefield",0] = "0.5 8 160 (7) Force field set to <color:ffffff>solid <color:ffffff>white";
$packSetting["forcefield",1] = "0.5 8 160 (6) Force field set to <color:ffffff>solid <color:ff4444> red";
$packSetting["forcefield",2] = "0.5 8 160 (5) Force field set to <color:ffffff>solid <color:44ff44> green";
$packSetting["forcefield",3] = "0.5 8 160 (4) Force field set to <color:ffffff>solid <color:4444ff> blue";
$packSetting["forcefield",4] = "0.5 8 160 (3) Force field set to <color:ffffff>solid <color:44ffff> cyan";
$packSetting["forcefield",5] = "0.5 8 160 (2) Force field set to <color:ffffff>solid <color:ff44ff> magenta";
$packSetting["forcefield",6] = "0.5 8 160 (1) Force field set to <color:ffffff>solid <color:ffff44> yellow";
$packSetting["forcefield",7] = "0.5 8 160 (7) Force field set to <color:44ff44>team-pass <color:ffffff>white";
$packSetting["forcefield",8] = "0.5 8 160 (6) Force field set to <color:44ff44>team-pass <color:ff4444> red";
$packSetting["forcefield",9] = "0.5 8 160 (5) Force field set to <color:44ff44>team-pass <color:44ff44> green";
$packSetting["forcefield",10] = "0.5 8 160 (4) Force field set to <color:44ff44>team-pass <color:4444ff> blue";
$packSetting["forcefield",11] = "0.5 8 160 (3) Force field set to <color:44ff44>team-pass <color:44ffff> cyan";
$packSetting["forcefield",12] = "0.5 8 160 (2) Force field set to <color:44ff44>team-pass <color:ff44ff> magenta";
$packSetting["forcefield",13] = "0.5 8 160 (1) Force field set to <color:44ff44>team-pass <color:ffff44> yellow";
$packSetting["forcefield",14] = "0.5 8 160 (7) Force field set to <color:ff4444>all-pass <color:ffffff>white";
$packSetting["forcefield",15] = "0.5 8 160 (6) Force field set to <color:ff4444>all-pass <color:ff4444> red";
$packSetting["forcefield",16] = "0.5 8 160 (5) Force field set to <color:ff4444>all-pass <color:44ff44> green";
$packSetting["forcefield",17] = "0.5 8 160 (4) Force field set to <color:ff4444>all-pass <color:4444ff> blue";
$packSetting["forcefield",18] = "0.5 8 160 (3) Force field set to <color:ff4444>all-pass <color:44ffff> cyan";
$packSetting["forcefield",19] = "0.5 8 160 (2) Force field set to <color:ff4444>all-pass <color:ff44ff> magenta";
$packSetting["forcefield",20] = "0.5 8 160 (1) Force field set to <color:ff4444>all-pass <color:ffff44> yellow";

$expertSettings["forcefield"] = 3;
$expertSetting["forcefield",0] = "Normal force field";
$expertSetting["forcefield",1] = "Cubic-replace force field";
$expertSetting["forcefield",2] = "Normal force field - no slowdown";
$expertSetting["forcefield",3] = "Cubic-replace force field - no slowdown";

$packSettings["gravfield"] = 4;
$packSetting["gravfield",0] = "4.25 8 500 normal slow";
$packSetting["gravfield",1] = "4.25 8 500 normal fast";
$packSetting["gravfield",2] = "4.25 8 500 reverse slow";
$packSetting["gravfield",3] = "4.25 8 500 reverse fast";
$packSetting["gravfield",4] = "4.25 8 500 zero gravity";
//
$packSetting["gravfield",5] = "4.25 8 500 fastfield";
$packSetting["gravfield",6] = "4.25 8 500 super fastfield";

$expertSettings["gravfield"] = 2;
$expertSetting["gravfield",0] = "Normal gravity field";
$expertSetting["gravfield",1] = "Cubic-replace gravity field (player's orientation)";
$expertSetting["gravfield",2] = "Cubic-replace gravity field (object's orientation)";

$packSettings["jumpad"] = 3;
$packSetting["jumpad",0] = "1000 10 boost";
$packSetting["jumpad",1] = "2500 25 boost";
$packSetting["jumpad",2] = "5000 50 boost";
$packSetting["jumpad",3] = "10000 100 boost";

$packSettings["tree"] = 13;
$packSetting["tree",0] = "normal 1";
$packSetting["tree",1] = "normal 2";
$packSetting["tree",2] = "normal 3";
$packSetting["tree",3] = "normal 4";
$packSetting["tree",4] = "greywood 1";
$packSetting["tree",5] = "greywood 2";
$packSetting["tree",6] = "greywood 3";
$packSetting["tree",7] = "greywood 4";
$packSetting["tree",8] = "greywood 5";
$packSetting["tree",9] = "cactus 1";
$packSetting["tree",10] = "cactus 2";
$packSetting["tree",11] = "misc 1";
$packSetting["tree",12] = "misc 2";
$packSetting["tree",13] = "pod 1";

$expertSettings["tree"] = 14;
$expertSetting["tree",0] = "0.0625";
$expertSetting["tree",1] = "0.125";
$expertSetting["tree",2] = "0.25";
$expertSetting["tree",3] = "0.5";
$expertSetting["tree",4] = "0.75";
$expertSetting["tree",5] = "1";
$expertSetting["tree",6] = "2";
$expertSetting["tree",7] = "3";
$expertSetting["tree",8] = "4";
$expertSetting["tree",9] = "5";
$expertSetting["tree",10] = "6";
$expertSetting["tree",11] = "7";
$expertSetting["tree",12] = "8";
$expertSetting["tree",13] = "9";
$expertSetting["tree",14] = "10";

$packSettings["crate"] = 12;
$packSetting["crate",0] = "(1) back pack";
$packSetting["crate",1] = "(2) small containment";
$packSetting["crate",2] = "(3) large containment";
$packSetting["crate",3] = "(4) compressor";
$packSetting["crate",4] = "(5) tubes";
$packSetting["crate",5] = "(6) quantum battery";
$packSetting["crate",6] = "(7) proton accelerator";
$packSetting["crate",7] = "(8) cargo crate";
$packSetting["crate",8] = "(9) magnetic cooler";
$packSetting["crate",9] = "(10) recycle unit";
$packSetting["crate",10] = "(11) fuel cannister";
$packSetting["crate",11] = "(12) wooden T2 box";
$packSetting["crate",12] = "(13) plasma router";

$packSettings["decoration"] = 15;
$packSetting["decoration",0] = "(1) banner unity";
$packSetting["decoration",1] = "(2) banner strength";
$packSetting["decoration",2] = "(3) banner honor";
$packSetting["decoration",3] = "(4) dead light armor";
$packSetting["decoration",4] = "(5) dead medium armor";
$packSetting["decoration",5] = "(6) dead heavy armor";
$packSetting["decoration",6] = "(7) statue base";
$packSetting["decoration",7] = "(8) light male statue";
$packSetting["decoration",8] = "(9) light female statue";
$packSetting["decoration",9] = "(10) heavy male statue";
$packSetting["decoration",10] = "(11) beowulf wreck";
$packSetting["decoration",11] = "(12) shrike wreck";
$packSetting["decoration",12] = "(13) billboard 1";
$packSetting["decoration",13] = "(14) billboard 2";
$packSetting["decoration",14] = "(15) billboard 3";
$packSetting["decoration",15] = "(16) billboard 4";

$packSettings["logoprojector"] = 7;
$packSetting["logoprojector",0] = "your teams logo";
$packSetting["logoprojector",1] = "Storm";
$packSetting["logoprojector",2] = "Inferno";
$packSetting["logoprojector",3] = "Starwolf";
$packSetting["logoprojector",4] = "Diamond Sword";
$packSetting["logoprojector",5] = "Blood Eagle";
$packSetting["logoprojector",6] = "Phoenix";
$packSetting["logoprojector",7] = "Bioderm";

$packSettings["switch"] = 6;
$packSetting["switch",0] = "5";
$packSetting["switch",1] = "10";
$packSetting["switch",2] = "25";
$packSetting["switch",3] = "50";
$packSetting["switch",4] = "100";
$packSetting["switch",5] = "150";
$packSetting["switch",6] = "200";

$expertSettings["switch"] = 4;
$expertSetting["switch",0] = "Normal switch";
$expertSetting["switch",1] = "Timed switch on";
$expertSetting["switch",2] = "Timed switch off";
$expertSetting["switch",3] = "Item Toggled";
$expertSetting["switch",4] = "Item Toggled [Disposal]";

$packSettings["light"] = 13;
$packSetting["light",0] = "(7) <color:ffffff>white";
$packSetting["light",1] = "(6) <color:ff4444>red";
$packSetting["light",2] = "(5) <color:44ff44>green";
$packSetting["light",3] = "(4) <color:4444ff>blue";
$packSetting["light",4] = "(3) <color:44ffff>cyan";
$packSetting["light",5] = "(2) <color:ff44ff>magenta";
$packSetting["light",6] = "(1) <color:ffff44>yellow";
$packSetting["light",7] = "(7) strobe <color:ffffff>white";
$packSetting["light",8] = "(6) strobe <color:ff4444>red";
$packSetting["light",9] = "(5) strobe <color:44ff44>green";
$packSetting["light",10] = "(4) strobe <color:4444ff>blue";
$packSetting["light",11] = "(3) strobe <color:44ffff>cyan";
$packSetting["light",12] = "(2) strobe <color:ff44ff>magenta";
$packSetting["light",13] = "(1) strobe <color:ffff44>yellow";

$packSettings["Door"] = 7;
$packSetting["Door",0] = "Normal Door";
$packSetting["Door",1] = "Toggle Door";
$packSetting["Door",2] = "Power Change Normal Door";
$packSetting["Door",3] = "Power Change Toggle Door";
$packSetting["Door",4] = "contact door";
$packSetting["Door",5] = "contact door owner door";
$packSetting["Door",6] = "contact door admin door";
$packSetting["Door",7] = "contact door if powered will be locked";

$expertSettings["Door"] = 5;
$expertSetting["Door",0] = "0 Seconds";
$expertSetting["Door",1] = "0.5 Seconds";
$expertSetting["Door",2] = "1 Second";
$expertSetting["Door",3] = "2 Seconds";
$expertSetting["Door",4] = "3 Seconds";
$expertSetting["Door",5] = "4 Seconds";

$packSettings["tripwire"] = 13;
$packSetting["tripwire",0] = "5 0";
$packSetting["tripwire",1] = "10 0";
$packSetting["tripwire",2] = "25 0";
$packSetting["tripwire",3] = "50 0";
$packSetting["tripwire",4] = "100 0";
$packSetting["tripwire",5] = "150 0";
$packSetting["tripwire",6] = "200 0";
$packSetting["tripwire",7] = "5 1";
$packSetting["tripwire",8] = "10 1";
$packSetting["tripwire",9] = "25 1";
$packSetting["tripwire",10] = "50 1";
$packSetting["tripwire",11] = "100 1";
$packSetting["tripwire",12] = "150 1";
$packSetting["tripwire",13] = "200 1";

$expertSettings["tripwire"] = 5;
$expertSetting["tripwire",0] = "Normal toggle on";
$expertSetting["tripwire",1] = "Normal toggle off";
$expertSetting["tripwire",2] = "Only turn on";
$expertSetting["tripwire",3] = "Only turn off";
$expertSetting["tripwire",4] = "Timed turn off";
$expertSetting["tripwire",5] = "Timed turn on";

$packSettings["escapepod"] = 7;
$packSetting["escapepod",0] = "1875";  //  12.25%
$packSetting["escapepod",1] = "3750";  //  25%
$packSetting["escapepod",2] = "5625";  //  37.5%
$packSetting["escapepod",3] = "7500";  //  50%
$packSetting["escapepod",4] = "9375";  //  62.5%
$packSetting["escapepod",5] = "11250"; //  75%
$packSetting["escapepod",6] = "13125"; //  87.5%
$packSetting["escapepod",7] = "15000"; // 100%

$expertSettings["telepad"] = 3;
$expertSetting["telepad",0] = "team only";
$expertSetting["telepad",1] = "any team";
$expertSetting["telepad",2] = "only transmit";
$expertSetting["telepad",3] = "only receive";

$packSettings["missilerack"] = 1;
$packSetting["missilerack",0] = "dumbfire missiles";
$packSetting["missilerack",1] = "seeking missiles";

$packSettings["VehiclePadPack"] = 2;
$packSetting["VehiclePadPack",0] = "Regular Vehicle Pad";
$packSetting["VehiclePadPack",1] = "Modern Vehicle Pad";
$packSetting["VehiclePadPack",2] = "Zigma's Smoke Shack";


//i modified the weapon mode change to work more like the pack settings
//easyer to edit and work whit
$WeaponSettings["modifier0"] = 0;
$WeaponSetting["modifier0",0] = "merge pieces";

$WeaponSettings["modifier1"] = 18;
$WeaponSetting["modifier1",0] = "DeployedSpine lsb";//0.125 0.166666 1
$WeaponSetting["modifier1",1] = "DeployedMSpine msb";//0.125 0.166666 1
$WeaponSetting["modifier1",2] = "DeployedwWall WalkWay";//0.125 0.166666 1
$WeaponSetting["modifier1",3] = "DeployedWall Bwall";//0.125 0.166666 1
$WeaponSetting["modifier1",4] = "DeployedSpine2 Dark Pad";//0.125 0.166666 1
$WeaponSetting["modifier1",5] = "DeployedCrate0 (crate1) back pack";//0.5 1 0.925
$WeaponSetting["modifier1",6] = "DeployedCrate1 (crate2) small containment";//0.16 0.5 0.488
$WeaponSetting["modifier1",7] = "DeployedCrate2 (crate3) large containment";//0.1 0.25 0.25
$WeaponSetting["modifier1",8] = "DeployedCrate3 (crate4) compressor";//1 1 1
$WeaponSetting["modifier1",9] = "DeployedCrate4 (crate5) tubes";//0.5 0.5 0.48
$WeaponSetting["modifier1",10] = "DeployedCrate5 (crate6) quantum battery";//0.25 0.25 0.25
$WeaponSetting["modifier1",11] = "DeployedCrate6 (crate7) proton accelerator";//0.25 0.5 0.5
$WeaponSetting["modifier1",12] = "DeployedCrate7 (crate8) cargo crate";//0.1255 0.249 0.246
$WeaponSetting["modifier1",13] = "DeployedCrate8 (crate9) magnetic cooler";//0.0835 0.167 0.1666
$WeaponSetting["modifier1",14] = "DeployedCrate9 (crate10) recycle unit";//1.25 1.25 0.48;
$WeaponSetting["modifier1",15] = "DeployedCrate10 (crate11) fuel cannister";//0.834 0.834 0.336
$WeaponSetting["modifier1",16] = "DeployedCrate11 (crate12) wooden T2 box";
$WeaponSetting["modifier1",17] = "DeployedCrate12 (crate13) plasma router";
$WeaponSetting["modifier1",18] = "DeployedDecoration6 (deco1) statue base";

$WeaponSettings["modifier2"] = 7;
$WeaponSetting["modifier2",0] = "+whole scale";
$WeaponSetting["modifier2",1] = "+x axis scale";
$WeaponSetting["modifier2",2] = "+y axis scale";
$WeaponSetting["modifier2",3] = "+z axis scale";
$WeaponSetting["modifier2",4] = "-whole scale";
$WeaponSetting["modifier2",5] = "-x axis scale";
$WeaponSetting["modifier2",6] = "-y axis scale";
$WeaponSetting["modifier2",7] = "-z axis scale";

$WeaponSettings["modifier3"] = 7;
$WeaponSetting["modifier3",0] = "move up";
$WeaponSetting["modifier3",1] = "move down";
$WeaponSetting["modifier3",2] = "+x axis move";
$WeaponSetting["modifier3",3] = "-x axis move";
$WeaponSetting["modifier3",4] = "+y axis move";
$WeaponSetting["modifier3",5] = "-y axis move";
$WeaponSetting["modifier3",6] = "+z axis move";
$WeaponSetting["modifier3",7] = "-z axis move";

$WeaponSettings["modifier4"] = 3;
$WeaponSetting["modifier4",0] = "0.1";
$WeaponSetting["modifier4",1] = "0.01";
$WeaponSetting["modifier4",2] = "0.001";
$WeaponSetting["modifier4",3] = "1";

$packSettings["window"] = 6;
$packSetting["window",0] = "<color:ffffff>White";
$packSetting["window",1] = "<color:ff4444>Red";
$packSetting["window",2] = "<color:44ff44>Green";
$packSetting["window",3] = "<color:4444ff>Blue";
$packSetting["window",4] = "<color:44ffff>Cyan";
$packSetting["window",5] = "<color:ff44ff>Magenta";
$packSetting["window",6] = "<color:ffff44>Yellow";

//list of smaller list
$WeaponSettings2["modifier"] = 5;//format :max  mode
$WeaponSetting2["modifier",0] = $WeaponSettings["modifier0"] SPC"Merge Pieces";
$WeaponSetting2["modifier",1] = $WeaponSettings["modifier1"] SPC"Swap Pad Texture";
$WeaponSetting2["modifier",2] = $packSettings["forcefield"]  SPC"Swap Force Field Texture";
$WeaponSetting2["modifier",3] = $WeaponSettings["modifier2"] SPC"Scale Pieces";
$WeaponSetting2["modifier",4] = $WeaponSettings["modifier3"] SPC"Nudge Pieces";
$WeaponSetting2["modifier",5] = $WeaponSettings["modifier4"] SPC"Modifier Scaler";

