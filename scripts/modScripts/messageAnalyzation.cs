// Message Analyzation (C) 2010 Robert MacGregor (AKA Vector)

// -----------------------------------------------------
// Basic Message Analyzation
// -----------------------------------------------------
function getMessageType(%string) //Determines the types of messages received
{
 %string = strLwr(%string);
 %word[0] = getWord(%string,0);
 %word[1] = getWord(%string,1);
 %word = %word[0] SPC %word[1];
 switch$(%word) //First, we analyze the first two words
 {
  case "what is" or "is this" or "who is" or "who are" or "who made":
  return "Question_Noun";

  case "how do" or "why do" or "why does":
  return "Question_Verb";

  case "i found" or "i am":
  return "Report_Noun";

  case "give me" or "get me" or "come get":
  return "Demand_Noun";
  
  case "send me":
  return "Request_Noun";

  case "make me":
  return "Crossover_NounVerb";
 }
 
 switch$(%word[0]) //Now we analyze the first word to get something more exact if the switch above fails
 {
  //General Things
  case "hi" or "hello":
  return "Greeting";

  case "bye" or "goodbye":
  return "Goodbye";
  
  case "hey": //Includes things like: HEY!
  return "Crossover_GreetingDistress";
  
  case "#SWEAR#":
  
  default:
  return "Unknown";
 }
}

function getMessageStructure(%string) //Returns the structure of messages received
{
 %count = getWordCount(%string);
 %string = strLwr(%string);
 %nouns = 50;
 %verbs = 50;
 %adjectives = 50;
 for (%i = 0; %i < %count; %i++)
 {
  for (%h = 0; %h < %nouns; %h++)
  {
   %string = strReplace(%string,$Message::Noun[%h],"NOUN");
  }
  for (%h = 0; %h < %verbs; %h++)
  {
   %string = strReplace(%string,$Message::Verb[%h],"VERB");
  }
  for (%h = 0; %h < %adjectives; %h++)
  {
   %string = strReplace(%string,$Message::Adjective[%h],"ADJECTIVE");
  }
 }
 return %string;
}

function doStringReplacements(%string)
{
 %string = strLwr(%string);
 for (%i = 0; %i < $Message::SwearWords::Count; %i++)
 {
  %string = strReplace(%string,$Message::SwearWords[%i],"#SWEAR#");
 }
}

function isSynonim(%word1,%word2) //Checks if both words have the same meaning in the bot's dictionary
{
 %nouns = 50;
 %verbs = 50;
 %adjectives = 50;
 //Check 1: We check if the first word exists
 for (%i = 0; %i < %nouns; %i++)
 {
  if ($Message::Noun[%word1] !$="")
  {
  }
 }
}

function spellCheckString(%string) //Spellchecks an entire string and attempts to ammend
{
 %count = getWordCount(%string);
}

$Message::SpellCheck::MinimumMatch = 0.8; //Must be an 80% match or more
function spellCheck(%string) //Actual spellCheck function, only checks one word
{
 %len = strLen(%string);
}

$Message::Sentence::Structure["compliment",0] = "ADJECTIVE NOUN"; //Like: good job, good shooting

$Message::Sentence::Structure["thanks",0] = "VERB NOUN"; //Like: thank you

$Message::Sentence::Structure["interaction",0] = "VERB NOUN"; //Like: help me, help it, build it

//Cuss words, the bot doesn't like these.
$Message::SwearWords[0] = "fuck";
$Message::SwearWords[1] = "bitch";
$Message::SwearWords[2] = "shit";
$Message::SwearWords[3] = "dike";
$Message::SwearWords::Count = 3;

//Word Arrays for Use in the bot's language. Also contains data used for spellchecking
//Word List Begin : Noun
$Message::Noun["programmer"] = "someone who tells a computer what to do";
$Message::Noun[0] = "programmer";
$Message::Synonims["programmer"] = "scripter";

//Word List Begin : Verb
$Message::Verb["kick"] = "to hit something with your leg";
$Message::Synonims["kick"] = "";
$Message::Verb[0] = "kick";
$Message::Harmful["kick"] = true;

$Message::Verb["lick"] = "to run your tongue up something";
$Message::Synonims["lick"] = "";
$Message::Verb[1] = "lick";
$Message::Harmful["lick"] = false;

$Message::Verb["stab"] = "to puncture someones skin";
$Message::Synonims["stab"] = "prick penetrate";
$Message::Verb[2] = "stab";
$Message::Harmful["stab"] = true;

//Word List Begin : Adjective
$Message::Adjective["red"] = "255 0 0";
$Message::Adjective[0] = "red";
$Message::Compliment["red"] = -1; //Isn't a compliment nor an insult -- get confused

$Message::Adjective["dumb"] = "";
$Message::Adjective[1] = "dumb";
$Message::Compliment["dumb"] = false; //Not a compliment, we take offense

$Message::Adjective["pretty"] = "describes something that is appealing to the eye";
$Message::Adjective[2] = "pretty";
$Message::Compliment["pretty"] = true; //Is a compliment, we may like this
$Message::Synonims["pretty"] = "beautiful sexy cute"; //The words don't have to be exact synonims, they just have to be alike in some way


