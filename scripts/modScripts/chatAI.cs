$ChatBot::Enabled = true; //Enabled?
$ChatBot::Response::MinSpeed = 700; //Our fastest Respnse Time
$ChatBot::Response::MaxSpeed = 1000; //Our slowest Response time
$ChatBot::Name = "Saphira"; //Our bot's name

//Anti-Scream Settings
$ChatBot::Admin::Antiscream::Enabled = true; //Enabled?
$ChatBot::Admin::Antiscream::TriggerPercent = 80; //Anything above 80% will be considered yelling

$ChatBot::Response["saphira"] = "I am Saphira."; //The message in the brackets [] should be all lowercase

//Randomized Response Arrays
$ChatBot::Response::Random["hey",0] = "Hello, #NAME#.";
$ChatBot::Response::Random["hey",1] = "Welcome, #NAME#.";
$ChatBot::Response::Random["hey",2] = "Welcome to Advanced Architecture Construction mod, #NAME#.";
$ChatBot::Response::Random::Count["hey"] = 2;

$ChatBot::Response::Random["info",0] = "This mod is known as Advanced Archtecture Construction mod scripted by Dark Dragon DX.";
$ChatBot::Response::Random::Count["info"] = 0;

//function getMessageType(%string) //Check our msg type
//{
// %check = getWord(%string,0);
 //switch$(%check) //First word checks
 //{
 // case "hey" or "hi" or "hola" or "kamitchawa":
 // return "hey";
// }
// switch$(%string) //Whole string Checks
// {
 // case "what mod is this" or "what is this" or "is this construction":
 // return "info";
 //}
//}

function doEscapeKeys(%sender, %string)
{
 %string = strReplace(%string,"#NAME#",%sender.namebase);
 return %string;
}

function stripUnwantedCharacters(%string)
{
 %string = stripChars(%string,"\c0\c1\c2\c3\c4\c5\c6\c7\c8!.,?");
 return %string;
}

function chatBotHandleMessage(%client,%message)
{
 if (!$ChatBot::Enabled)
 return;

 %message = strLwr(stripUnwantedCharacters(%message));
 
 if ($ChatBot::Response[%message] !$= "")
 {
  %response = doEscapeKeys(%client,$ChatBot::Response[%message]);
  schedule(getRandom($ChatBot::Response::MinSpeed,$ChatBot::Response::MaxSpeed),0,"messageAll",'msgAll',"\c4" @ $ChatBot::Name @ ":" SPC %response);
 }
 else
 {
  %type = getMessageType(%message);
  %random = doEscapeKeys(%client,$ChatBot::Response::Random[%type,getRandom(0,$ChatBot::Response::Random::Count[%type])]);
  schedule(getRandom($ChatBot::Response::MinSpeed,$ChatBot::Response::MaxSpeed),0,"messageAll",'msgAll',"\c4" @ $ChatBot::Name @ ":" SPC %random);
 }
}

//Auto Installer
deactivatePackage(Saphira);
package Saphira{
 function chatMessageAll( %sender, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10)
 {
  parent::chatMessageAll( %sender, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10);
  chatBotHandleMessage(%sender,%a2);
 }
};
activatePackage(Saphira);
