// File Processing (C) 2010 Robert MacGregor (AKA Dark Dragon DX)

// -----------------------------------------------------
// Basic file functions
// -----------------------------------------------------
function getFileBuffer(%file)
{
 if (!IsFile(%file))
 return "Not existant.";

 new FileObject(FileBuffer);
 FileBuffer.openForRead(%file);

 while (!FileBuffer.isEOF())
 {
  %buffer = FileBuffer.readLine() @ "\n";
 }
 FileBuffer.detach();
 return %buffer; //Long string. >.>
}

function getLine(%file, %line)
{
 if (!IsFile(%file))
 return "Not existant.";

 new FileObject(FileLine);
 FileLine.openForRead(%file);

 for (%i = 0; %i < %line; %i++)
 {
  %line = FileLine.readLine();
 }
 FileLine.detach();

 return %line;
}

// -----------------------------------------------------
// Bound Functions
// -----------------------------------------------------
function fileObject::Detach(%this) //Detaches fileObject from file & deletes
{
 %this.close();
 %this.delete();
}




