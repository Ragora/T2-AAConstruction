// String Processing (C) 2010 Robert MacGregor (AKA Dark Dragon DX)

//------------------------------------------------------------------------------
function textToHash(%text)
{
 %fileObj = new fileObject();
 %fileObj.openForWrite("Temp.txt");
 %fileObj.writeLine(%text);
 %fileObj.detach();
 %hash = getFileCRC("temp.txt");
 deleteFile("Temp.txt");
 return %hash;
}

//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function strReverse(%string)
{
 %len = StrLen(%string);
 %rstring = "";

 for (%i = 0; %i < %len; %i++)
 {
  %rstring = getSubStr(%string,%i,1) @ %rstring;
 }
 return %rstring;
}

//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function subStrInsert(%string,%insert,%slot)
{
 %seg = getSubStr(%string,0,%slot);
 %seg = %seg @ %insert;
 %string = %seg @ getSubStr(%string,%slot,strLen(%string));

 return %string;
}

//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function subStrRemove(%string,%slot)//Minimum: 1
{
 %half2 = getSubStr(%string,%slot,strLen(%string));
 %half1 = getSubStr(%string,0,%slot-1);
 return %half1 @ %half2;
}

//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function strMove(%string,%factor)
{
 %len = GetWordCount(%string);

 for (%i = 0; %i < %len; %i++)
 {
  %sub = getWord(%string,%i);
  %move = subStrInsert(%move,%sub,%i);
 }

 return %move;
}

//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function subStrMove(%string,%factor)
{
 %len = strLen(%string);

 for (%i = 0; %i < %len; %i++)
 {
  %sub = getSubStr(%string,%i,1);
  %move = subStrInsert(%move,%sub,%factor);
 }

 return %move;
}

//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function subStrScramble(%string)
{
 %len = strLen(%string);

 for (%i = 0; %i < %len; %i++)
 {
  %sub = getSubStr(%string,%i,1);
  %scramble = subStrInsert(%scramble,%sub,getRandom(0,%len));
 }

 return %scramble;
}
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function strSplit(%string)
{
 %count = strLen(%string);
 %div = %count / 2;

 return getSubStr(%string,0,%div) @ " | " @ getSubStr(%string,%div,%count);
}

//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function stripSpaces(%string)
{
 return strReplace(%string," ","");
}

//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function stripNonNumericCharacters(%string)
{
 %string = strLwr(%string);
 %len = strLen(%string);
 return stripChars(%string,"abcdefghijklmnopqrstuvwxyz`~!@#$%^&*()-_=+\|}]{[/?.>,<;:");
}

//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function getSubStrOccurance(%string,%search)
{
 %len = strLen(%string);
 %srLen = strLen(%search);
 %count = 0;
 for (%i = 0; %i < %len; %i++)
 {
  %strSearch = strStr(%string,%search);

  if (%strSearch != -1) //It exists somewhere in the string
  {
   %count++;
   %string = getSubStr(%string,%strSearch+%srLen,%len);
  }
  else
  return %count SPC %strSearch+%srLen;
}
return %count SPC %strSearch+%srLen;
}

function getSubStrPos(%string,%str,%num)
{
 %len = strLen(%string);

 %subC = 0;
 for (%i = 0; %i < %len; %i++)
 {
  %sub = getSubStr(%string,%i,1);

  if (%sub $= %str)
  {
   %subC++;
   if (%subC == %num)
   break;
  }
 }
 return %i;
}

//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
function translateCharacters(%string)
{
 %len = strLen(%string);
 %char = getSubStr(%string,%len-1,1);
 %string = subStrRemove(%string,%len);
 %string = %char @ %string;
}

//------------------------------------------------------------------------------
function getFileNameFromString(%string)
{
 return getSubStr(%string,getSubStrPos(%string,"/",getSubStrOccurance(%string, "/"))+1,strLen(%string));
}

//------------------------------------------------------------------------------
