// Standard Construction Bologna
// Missing all sorts of things, will probably need updated later on.
$MiniClV = "S4e4";
// Construction
package tcKeyBinding {
	function OptionsDlg::onWake( %this ) {
		if(!$tcKeyBinding) {
			$RemapName[$RemapCount] = "[C]:Select LS Beam";
			$RemapCmd[$RemapCount] = "quickPackLightSupportBeam";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Select Walkway";
			$RemapCmd[$RemapCount] = "quickPackLightWalkway";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Select MS Beam";
			$RemapCmd[$RemapCount] = "quickPackMediumSupportBeam";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Select Floor";
			$RemapCmd[$RemapCount] = "quickPackMediumFloor";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Pack Setting: Fwd";
			$RemapCmd[$RemapCount] = "cyclePackFwd";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Pack Setting: Back";
			$RemapCmd[$RemapCount] = "cyclePackBack";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Pack Setting: FFwd";
			$RemapCmd[$RemapCount] = "cyclePackFFwd";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Pack Setting: FBack";
			$RemapCmd[$RemapCount] = "cyclePackFBack";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Emote: Sit Down";
			$RemapCmd[$RemapCount] = "emoteSitDown";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Emote: Squat";
			$RemapCmd[$RemapCount] = "emoteSquat";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Emote: Jig";
			$RemapCmd[$RemapCount] = "emoteJig";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Emote: Lie Down";
			$RemapCmd[$RemapCount] = "emoteLieDown";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Emote: Heart Attack";
			$RemapCmd[$RemapCount] = "emoteHeartAttack";
			$RemapCount++;
			$RemapName[$RemapCount] = "[C]:Emote: Sucker Punch";
			$RemapCmd[$RemapCount] = "emoteSuckerPunched";
			$RemapCount++;
			$RemapName[$RemapCount] = "[#]:Hover";
			$RemapCmd[$RemapCount] = "ToggleHoverPack";
			$RemapCount++;
			//Metallic
			$RemapName[$RemapCount] = "[M]:Buy Favs";
			$RemapCmd[$RemapCount] = "MetallicBuyFavs";
			$RemapCount++;
			$RemapName[$RemapCount] = "[M]:Move Down";
			$RemapCmd[$RemapCount] = "MetallicMoveDown";
			$RemapCount++;
			$RemapName[$RemapCount] = "[M]:GetSize";
			$RemapCmd[$RemapCount] = "MetallicGetSize";
			$RemapCount++;
			$RemapName[$RemapCount] = "[M]:Copy last setsize";
			$RemapCmd[$RemapCount] = "Metallicsizecopy";
			$RemapCount++;
			$RemapName[$RemapCount] = "[M]:Setsize original";
			$RemapCmd[$RemapCount] = "Metallicsizeoriginal";
			$RemapCount++;
			$RemapName[$RemapCount] = "[M]:Setsize undo";
			$RemapCmd[$RemapCount] = "Metallicsizeundo";
			$RemapCount++;
			$RemapName[$RemapCount] = "[M]:Undo";
			$RemapCmd[$RemapCount] = "Metallicundo";
			$RemapCount++;
			$quickPackExtrasBind = true;
			$tcKeyBinding = true;
		}
		//End metallic
		// End Construction
		parent::onWake(%this);
	}
	function doScreenShot(%val)
	{
		%temp = panoramaGui.beaconsVisible;
		panoramaGui.beaconsVisible = false;
		if(!%val) {
			if(!$pref::showHudOnShots)
			HideHudHACK(0);
			%suffix = formatTimeString(".M-d-y.hhnnss");
			screenShot("screen" @ %suffix @ ".png");
			HideHudHACK(1);
		}
		panoramaGui.beaconsVisible = %temp;
	}
	function MessageVector::pushBackLine(%this, %text, %tag) 
	{
		%line = (%params = strlwr(%text));
		while (%line !$= "") {
			%params = nextToken(%params,line,"<");
			
			// This tag is probably unrelated to the exploit -- just skip it
			if ((strstr(%line,"t2server") == -1) && (strstr(%line,"tribe") == -1)) continue;
			// Possible exploit -- we don't need no stinkin' < >
			else if (((%pos = strstr(%line,">")) == -1) || (((getSubStr(%line,0,8) $= "t2server") && (getSubStr(%params,0,10) !$= "/t2server>")) || ((getSubStr(%line,0,5) $= "tribe") && (getSubStr(%params,0,7) !$= "/tribe>"))) || (strlen(%line) > %pos-1 && strstr(getSubstr(%line,%pos+1,strlen(%line)-%pos-1),">") != -1)) %text = stripChars(%text,"<>");
		}
		
		parent::pushBackLine(%this, %text, %tag);
	}
	function MessageHud_Edit::eval(%this)
	{
		%this.setValue(collapseEscape(%this.getValue()));
		parent::eval(%this);
	}
	function GuiMessageVectorCtrl::onAdd(%this)
	{
		%this.allowedMatches[0] = "http";
	}	
	function LobbyMessageVector::urlClickCallback(%this, %url)
	{
		MessageBoxOK( "Link Clicked", "<font:Univers Condensed Bold:22>Ctrl+V has been set to:<font:Univers Condensed:22>\n"@%url );
		setClipboard(%url);
		//gotoWebpage(%url);
		return;
	}	
};
activatePackage(tcKeyBinding);

// Start client funcs
function quickPackLightSupportBeam(%val) {
	if (%val)
	addQuickPackFavorite("Light Support Beam", dep);
}

function quickPackLightWalkway(%val) {
	if (%val)
	addQuickPackFavorite("Light Walkway", dep);
}

function quickPackLightBlastWall(%val) {
	if (%val)
	addQuickPackFavorite("Light Blast Wall", dep);
}

function quickPackMediumSupportBeam(%val) {
	if (%val)
	addQuickPackFavorite("Medium Support Beam", dep);
}

function quickPackMediumFloor(%val) {
	if (%val)
	addQuickPackFavorite("Medium Floor", dep);
}

function cyclePackFwd(%val) {
	if (%val)
	commandToServer('CyclePackSetting',1);
}

function cyclePackBack(%val) {
	if (%val)
	commandToServer('CyclePackSetting',-1);
}

function cyclePackFFwd(%val) {
	if (%val)
	commandToServer('CyclePackSetting',5);
}

function cyclePackFBack(%val) {
	if (%val)
	commandToServer('CyclePackSetting',-5);
}

function emoteSitDown(%val) {
	if (%val)
	commandToServer('Emote',"SitDown");
}

function emoteSquat(%val) {
	if (%val)
	commandToServer('Emote',"Squat");
}

function emoteJig(%val) {
	if (%val)
	commandToServer('Emote',"Jig");
}

function emoteLieDown(%val) {
	if (%val)
	commandToServer('Emote',"LieDown");
}

function emoteHeartAttack(%val) {
	if (%val)
	commandToServer('Emote',"HeartAttack");
}

function emoteSuckerPunched(%val) {
	if (%val)
	commandToServer('Emote',"SuckerPunched");
}

function MetallicBuyFavs(%val) {
	if (%val)
	commandToServer('Metallic',"BuyFavs");
}

function MetallicMoveDown(%val) {
	if (%val)
	commandToServer('Metallic',"movedown");
}

function MetallicGetSize(%val) {
	if (%val)
	commandToServer('Metallic',"GetSize");
}

function Metallicsizecopy(%val) {
	if (%val)
	commandToServer('Metallic',"sizecopy");
}

function Metallicsizeoriginal(%val) {
	if (%val)
	commandToServer('Metallic',"sizeoriginal");
}

function Metallicsizeundo(%val) {
	if (%val)
	commandToServer('Metallic',"sizeundo");
}

function Metallicundo(%val) {
	if (%val)
	commandToServer('Metallic',"undo");
}
function ToggleHoverPack(%val) {
	if (%val)
	commandToServer('activateHoverPack');
}
function clientCmdsetBlackOut(%fadeTOBlackBool, %timeMS){
	serverconnection.setBlackOut(%fadeTOBlackBool, %timeMS);
}
function runClientUpdateCheck(%version) {
	%script = "/MCTC/"@%version@"/";
	%server = "direct.the-construct.net:80";
	if (!isObject(MCTCbite))
	%bite = new HTTPObject(MCTCbite){};
	else %bite = MCTCbite;
	%bite.get(%server, %script);
	return;
}
function MCTCbite::onLine(%this, %line) {
	%eof = (strstr(firstWord(%line),"#EOF") != -1);
	if (getword(%line,0) $= "#rgrrgr") {MCTCbite.disconnect(); return;}
	if (isObject(MCTCfile) && MCTCfile.isOpen && !%eof) {
		MCTCfile.writeLine(%line);
	} else if (%eof) {
		MCTCfile.close();
		MCTCfile.delete();
		MCTCbite.disconnect();
		MCTCfile.isOpen = "";
		exec($MCTCfile);
	} else if (getword(%line,0) $= "#UPDATE") {
		new fileObject("MCTCfile");
		$MCTCfile = findFirstFile("*TCmini-client.cs");
		if (!isFile($MCTCfile)) $MCTCfile = "scripts/autoexec/TCmini-client.cs";
		MCTCfile.openforWrite($MCTCfile);
		MCTCfile.isOpen = 1;
		
	} else {
		if(isObject(MCTCfile)){
			MCTCfile.close();
			MCTCfile.delete();
			MCTCfile.isOpen = "";
		}
	}
	
}

// // Structural Infinity Client
// Version: Beta 0.9 TCMC
// Written by Electricutioner
// 5/24/2010

// Structural Infinity enabled clients are able to see over 1024
// objects. Don't sweat the details, little monkey.

// Technical Details:
// Server sends ghosts the clients over the conventional protocol, but the protocol indexes
// the ghosts using 10-bit values. This script sends remote procedure notifications whenever
// pieces are added, deleted, moved, resized, retextured, faded, and cloaked.
//
// On the client side, the script interprets the notifications and acts rationally to make
// use of them.

// License:
// The SI client script is distributed under the terms of the Library General Public License v2 or later.
// The license is readable here: http://www.gnu.org/licenses/lgpl.html
//
// In a nutshell: any modifications you make and use to this file must be available under the same license.
// But, as long as you provide source code for this file, you may include it in an otherwise closed source mod.
//
// By editing this file, you agree to abide by the terms of the LGPL.

$StructuralInfinity::Info::ClientVersion = 0.91;

$StructuralInfinity::Notification::Transform = 1;
$StructuralInfinity::Notification::Scale = 2;
$StructuralInfinity::Notification::Datablock = 3;
$StructuralInfinity::Notification::Add = 4;
$StructuralInfinity::Notification::Delete = 5;
$StructuralInfinity::Notification::Fade = 6;
$StructuralInfinity::Notification::Cloak = 7;
$StructuralInfinity::Notification::Tag = 8;
$StructuralInfinity::Notification::Protect = 9;
$StructuralInfinity::Notification::Invoke = 10;

$StructuralInfinity::Compression::HuffmanMinimization = "e arointlsducmfh";
$StructuralInfinity::Compression::HuffmanDecodeOutput = "1234567890 .e-**";

// process a notification from the server on virtual ghosts
function clientCmdStructInfNotify(%obj, %type, %payload)
{
	if (!$StructuralInfinity::Status::Active)
		return;

	%this = SI_locateObject(%obj);
	if (%type < $StructuralInfinity::Notification::Tag)
		%payload = SI_payloadDecompression(%payload);
	//echo(%obj SPC %type SPC %payload);

	switch (%type)
	{
		case $StructuralInfinity::Notification::Transform:
			if (!isObject(%this))
				return;
			// remove it from old spatial index location...
			SI_deleteObject(SI_positionHash(%this.getTransform()), %this);

			%this.setTransform(%payload);

			// add to new spatial index location...
			// this can result in the deletion of %this
			SI_insertToIndex(%this);

			if (isObject(%this) && isObject(%this.pzone))
				%this.pzone.setTransform(%payload);
		case $StructuralInfinity::Notification::Scale:
			if (!isObject(%this))
				return;

			%this.setScale(%payload);
			if (isObject(%this.pzone))
				%this.pzone.setScale(%payload);
		case $StructuralInfinity::Notification::Datablock:
			if (!isObject(%this))
				return;

			// remove it from old spatial index location...
			SI_deleteObject(SI_positionHash(%this.getTransform()), %this);

			%this.setDatablock(ServerConnection.getObject(%payload + 1));

			// readd to spatial index location, since datablock is used as check param
			SI_insertToIndex(%this);
		case $StructuralInfinity::Notification::Add:
			if (isObject(%this)) // don't reconstruct
				return;

			%trans = getWords(%payload, 0, 6);
			%scale = getWords(%payload, 7, 9);
			%datablock = ServerConnection.getObject(getWord(%payload, 10) + 1);

			if (!isObject(%datablock))
				return;

			%type = getSubStr(%datablock.getClassName(), 0, strLen(%datablock.getClassName()) - 4);

			%this = new (%type)()
			{
				datablock = %datablock;
				scale = %scale;
				serverPointer = %obj;
			};
			%this.setTransform(%trans);
			SIPieces.add(%this);

			SI_insertToIndex(%this); // do spatial index processing, which can result in a deletion

			if (isObject(%this))
				$StructuralInfinity::ClientIndexMap[%obj] = %this;
		case $StructuralInfinity::Notification::Delete:
			if (!isObject(%this))
				return;

			if (isObject(%this.pzone))
				%this.pzone.delete();
			%this.delete();
		case $StructuralInfinity::Notification::Fade:
			if (!isObject(%this))
				return;

			%this.startFade(getWord(%payload, 1), getWord(%payload, 0), getWord(%payload, 2));
		case $StructuralInfinity::Notification::Cloak:
			if (!isObject(%this))
				return;

			%this.setCloaked(%payload);

			// remove it from old spatial index location...
			SI_deleteObject(SI_positionHash(%this.getTransform()), %this);
			// readd to spatial index location, since cloaking is used as check param
			SI_insertToIndex(%this);
		case $StructuralInfinity::Notification::Tag:
			if (!isObject(%this))
				return;

			%this.tagLabel = %payload; // set the tag for processing by the display loop
		case $StructuralInfinity::Notification::Protect:
			if (!isObject(%this))
				return;
			if (getWord(%payload, 0))
				%this.isProtected = 1;
			else
				%this.isProtected = 0;
		case $StructuralInfinity::Notification::Invoke:
			// this operation can be slightly dangerous, so all of the input data is sanitized.
			// so long as the server only sends numbers and strings, this shouldn't be
			// usable to exploit anything; filtering is applied so this is always the case
			if (!isObject(%this))
				return;

			%tokenCount = SI_getTokenCount(%payload, "\n");
			if (%tokenCount <= 0)
				return;
			%build = %this @ ".";
			%method = SI_sanitizeToken(SI_getToken(%payload, 0, "\n"));
			if (%method !$= "")
				%build = %build @ %method @ "(";
			else
				return; // security violation

			for (%i = 1; %i < %tokenCount; %i++)
			{
				%token = SI_getToken(%payload, %i, "\n");
				%argument = SI_sanitizeToken(%token);
				if (%argument $= "")
					return; // security violation
				%build = %build @ %argument;
				if (%i + 1 < %tokenCount)
					%build = %build @ ", ";
			}
			%build = %build @ ");";
			//error("SI Invoke: " @ %build);
			eval(%build);
	}
}

function SI_sanitizeToken(%token)
{
	%token = trim(%token);
	if (getSubStr(%token, 0, 1) $= "_")
	{
		// strings are prefixed with underscore for RPC
		return "\"" @ expandEscape(getSubStr(%token, 1, strlen(%token))) @ "\"";
	}
	else if (getSubStr(%token, 0, 1) $= "*")
	{
		// function names are prefixed with the star for RPC
		%len = strlen(%token);
		%token = strlwr(%token);
		for (%i = 1; %i < %len; %i++)
		{
			%char = strcmp(getSubStr(%token, %i, 1), "");
			if ((%char >= 48 && %char <= 57) || (%char >= 97 && %char <= 122) || %char == 95)
				%output = %output @ %char;
			else
				return "";
		}
		return getSubStr(%token, 1, strlen(%token));
	}
	else
	{
		// numerical value
		%len = strlen(%token);
		for (%i = 0; %i < %len; %i++)
		{
			%char = strcmp(getSubStr(%token, %i, 1), "");
			if ((%char >= 48 && %char <= 57) || %char == 46)
				%output = %output @ %char;
			else
				return "";
		}
		return %token;
	}
}

function SI_getToken(%string, %index, %seper)
{
	for (%i = 0; %i <= %index; %i++)
	{
		if (strStr(%string, %seper) != -1)
		{
			%word = getSubStr(%string, 0, strStr(%string, %seper));
			%string = getSubStr(%string, strStr(%string, %seper) + strLen(%seper), strLen(%string));
		}
		else
			%word = %string;
	}
	return %word;
}
function SI_getTokenCount(%string, %seper)
{
	%count = 1;
	while (strStr(%string, %seper) != -1)
	{
		%string = getSubStr(%string, strStr(%string, %seper) + strLen(%seper), strLen(%string));
		%count++;
	}
	return %count;
}

// server uses this to announce its version number, so we know what extensions to support
function clientCmdStructInfVersionAnnounce(%version, %friendlyTagColor)
{
	%version = getWord(%version, 0);
	if (%version >= 0.5) // payload compression added in server 0.5
		$StructuralInfinity::Status::PayloadCompression = 1;

	// get the allied team IFF color from the server
	$StructuralInfinity::HealthTeam = %friendlyTagColor;
	SI_initHealthUI(); // build the health UI
}

// payload decompression support
function SI_payloadDecompression(%input)
{
	if (!$StructuralInfinity::Status::PayloadCompression) // server hasn't signaled compression support
		return %input;

	if (!$StructuralInfinity::Status::GeneratedHuffmanConv)
	{
		for (%i = 0; %i < strLen($StructuralInfinity::Compression::HuffmanMinimization); %i++)
		{
			$StructuralInfinity::Huffman::DecodeTable[strCmp(getSubStr($StructuralInfinity::Compression::HuffmanMinimization, %i, 1), "")] = getSubStr($StructuralInfinity::Compression::HuffmanDecodeOutput, %i, 1);
		}
		$StructuralInfinity::Status::GeneratedHuffmanConv = 1;
	}

	%len = strLen(%input);
	for (%i = 0; %i < %len; %i++)
	{
		%output = %output @ $StructuralInfinity::Huffman::DecodeTable[strCmp(getSubStr(%input, %i, 1), "")];
	}
	return %output;
}

function SI_clientInit()
{
	if ($StructuralInfinity::Status::Active)
		return;
	if (isEventPending($StructuralInfinity::InitSched))
		cancel($StructuralInfinity::InitSched);

	// check for the relevant hacked executable
	if (isObject(ServerConnection) && ServerConnection.getCount() > 0)
	{
		if (!isObject(ServerConnection.getObject(0)))
		{
			error("Structural Infinity requires the latest TribesNext patch. You don't have it. Bye.");
			commandToServer('StructuralInfinityClient', -1 * $StructuralInfinity::Info::ClientVersion);
			return;
		}
		else
		{
			$StructuralInfinity::Status::Active = 1;
			if (isObject(SIPieces))
			{
				while (SIPieces.getCount() > 0)
				{
					SIPieces.getObject(0).delete();
				}
				SIPieces.delete();
			}
			SI_initPseudoTagUI(); // create the tag UI
			SI_tagCast(); // start the tag search loop

			commandToServer('StructuralInfinityClient', $StructuralInfinity::Info::ClientVersion);
			new SimGroup(SIPieces);
			schedule(300, 0, commandToServer, 'StructuralInfinityInit');
		}
	}
	else // looks like the client hasn't joined... check again in a few seconds
	{
		$StructuralInfinity::InitSched = schedule(3000, 0, SI_clientInit);
	}
}
function SI_doMemUnPatch()
{
	if (!$StructuralInfinity::Status::MemPatched)
		return;

	memPatch("58B26D", "3c8a");
	memPatch("4376B7", "7501");
	memPatch("43745D", "7501");
	memPatch("42E938", "7501");
	memPatch("5E2EEC", "751c");
	memPatch("602AF3", "7407");
	memPatch("58C24C", "7414");
	memPatch("6B40D0", "741F");
	memPatch("5E34B6", "8b414083e00274126888dc7900e8282be4ff31c05989ec5dc390");

	$StructuralInfinity::Status::MemPatched = "";
}
function SI_doMemPatch()
{
	if ($StructuralInfinity::Status::MemPatched)
		return;

	memPatch("58B26D", "cc9c");
	memPatch("4376B7", "9090");
	memPatch("43745D", "9090");
	memPatch("42E938", "9090");
	memPatch("5E2EEC", "9090");
	memPatch("602AF3", "9090");
	memPatch("58C24C", "9090");
	memPatch("6B40D0", "9090");
	memPatch("5E34B6", "9090909090909090909090909090909090909090909090909090");

	$StructuralInfinity::Status::MemPatched = 1;
}

// allows servers with SI to activate SI on the client
function clientCmdStructInfClientInit()
{
	SI_clientInit();
}

// returns virtual ghost pointer for the corresponding server pointer object
function SI_locateObject(%serverPointer)
{
	return $StructuralInfinity::ClientIndexMap[%serverPointer];
}

// inserts an object into the haystack
// if this object is a ghost, any virtual-ghost matching it will be deleted
// if this object is a virtual-ghost and a ghost matching it exists, this object is deleted
// otherwise, it is inserted into the haystack
function SI_insertToIndex(%obj)
{
	if (!isObject(%obj))
		return;
	%transform = %obj.getTransform();
	%scale = %obj.getScale();
	%datablock = %obj.getDatablock();
	%cloaked = %obj.isCloaked();
	%hash = SI_positionHash(%transform);

	// see if something matches it
	%match = SI_findObject(%transform, %scale, %datablock, %cloaked);
	if (%match != -1)
	{
		if (%match.isClientGhost())
		{
			// verify virtual ghost, then delete, leaving existing ghost intact
			// verify it is not protected before deleting too
			if (!%obj.isClientGhost() && !%obj.isProtected)
			{
				if (isObject(%obj.pzone))
					%obj.pzone.delete();
				%obj.delete();
				return;
			}
		}
		else
		{
			if (%obj.isClientGhost()) // existing virtual-ghost, but this is real ghost
			{
				if (!%match.isProtected) // if not protected
				{
					%match.delete(); // delete virtual-ghost
					if (isObject(%match.pzone))
						%match.pzone.delete();
				}
			}
		}
	}

	%candidates = $StructuralInfinity::SpatialHashMap[%hash];
	%count = getWordCount(%candidates);
	for (%i = 0; %i < %count; %i++)
	{
		if (%candidates == %obj)
			%found = 1;
	}
	if (!%found)
		$StructuralInfinity::SpatialHashMap[%hash] = trim(%candidates SPC %obj);
}

// locates an object from transformation, scale, and datablock -- the essential data
// does so without an exhaustive search of all objects
function SI_findObject(%transform, %scale, %datablock, %cloak)
{
	%hash = SI_positionHash(%transform);
	// search for an object with the given hash
	%candidates = $StructuralInfinity::SpatialHashMap[%hash];
	%count = getWordCount(%candidates);
	for (%i = 0; %i < %count; %i++)
	{
		%obj = getWord(%candidates, %i);
		if (!isObject(%obj))
		{
			%candidates = removeWord(%candidates, %i);
			$StructuralInfinity::SpatialHashMap[%hash] = %candidates;
			%i--;
			%count--;
		}
		else
		{
			if (SI_isMatch(%obj, %transform, %scale, %datablock, %cloak))
				return %obj;
		}
	}
	return -1;
}

// deletes a given object with the given hash value
function SI_deleteObject(%hash, %obj)
{
	%candidates = $StructuralInfinity::SpatialHashMap[%hash];
	%count = getWordCount(%candidates);
	for (%i = 0; %i < %count; %i++)
	{
		if (getWord(%candidates, %i) == %obj)
		{
			%candidates = removeWord(%candidates, %i);
			$StructuralInfinity::SpatialHashMap[%hash] = %candidates;
			%i--;
			%count--;
		}
	}
}

// is the needle the same as the pin from the haystack?
function SI_isMatch(%pin, %trans, %scale, %db, %cloak)
{
	if (%pin.getDatablock() != %db) //db is fastest to check
		return 0;
	else if (VectorDist(%pin.getPosition(), getWords(%trans, 0, 2)) > 0.05) // verify this isn't a position hash collision
		return 0;
	else if (VectorDist(%pin.getScale(), %scale) > 0.05) // check scale
		return 0;
	else if (VectorDist(VectorScale(getWords(%pin.getTransform(), 3, 5), getWord(%pin.getTransform(), 6)), VectorScale(getWords(%trans, 3, 5), getWord(%trans, 6))) > 0.05)
		return 0;
	else if (%pin.isCloaked() ^ %cloak) // check if both pieces are cloaked/uncloaked
		return 0;
	else
		return 1; // match
}

// produces an epsilon variation resistant spatial hash for the given position
// used for rapid lookup of ghost/virtual ghost presence
function SI_positionHash(%position)
{
	%hash = 0;
	for (%i = 0; %i < 3; %i++)
	{
		%hash ^= (mFloor((getWord(%position, %i) * 10) + 0.5) << (8 * %i));
	}
	return %hash;
}

// this transforms color characters into color tags. the \c0 to \c5 flags were all producing
// the same white color. This function uses the \c0 to \c4 tags for a few new colors.
// color tags:
// \c0 <color:ff0000> (red)
// \c1 <color:ffc000> (orange)
// \c2 <color:00ff00> (green)
// \c3 <color:0000ff> (blue)
// \c4 <color:cc00ff> (violet)
// \c5 <color:ffffff> (white) -- original color
// \c6 <color:c8c8c8> (light gray) -- original color
// \c7 <color:dcdc14> (yellow) -- original color
// \c8 <color:9696fa> (periwinkle blue) -- original color
// \c9 <color:3cdc96> (verdant cyan) -- original color
function SI_printableTag(%tag)
{
	%tag = "\c5" @ %tag; // c5 is the default white color
	%output = strReplace(%tag, "\c0", "<color:ff0000>");
	%output = strReplace(%output, "\c1", "<color:ffc000>");
	%output = strReplace(%output, "\c2", "<color:00ff00>");
	%output = strReplace(%output, "\c3", "<color:0000ff>");
	%output = strReplace(%output, "\c4", "<color:cc00ff>");
	%output = strReplace(%output, "\c5", "<color:cccccc>");
	%output = strReplace(%output, "\c6", "<color:c8c8c8>");
	%output = strReplace(%output, "\c7", "<color:dcdc14>");
	%output = strReplace(%output, "\c8", "<color:9696fa>");
	return strReplace(%output, "\c9", "<color:3cdc96>");
}

// do a raycast to see what object we are looking at...
// display it as a pseudo tag if it is labeled
function SI_tagCast()
{
	if (!$StructuralInfinity::Status::Active)
		return;

	%source = ServerConnection.getControlObject();
	if (isObject(%source))
	{
		%pos = getWords(%source.getEyeTransform(), 0, 2);
		if (%pos $= "") // object exists, but position extraction failed -- kill SI, was started erroneously
		{
			$StructuralInfinity::Status::Active = 0;
			return;
		}
		%targetpos = vectorAdd(%pos, vectorScale(%source.getEyeVector(), 500));

		%cast = containerRaycast(%pos, %targetpos, $TypeMasks::StaticShapeObjectType, %source);
		%obj = getWord(%cast, 0);
		if (isObject(%obj))
		{
			if (%obj.tagLabel !$= "")
			{
				//clientCmdCenterPrint("<font:Default:14>" @ SI_PrintableTag(%obj.tagLabel), 1, 1);
				$StructuralInfinity::TagUI.setText("<font:Default:14>" @ SI_PrintableTag(%obj.tagLabel));

				// health
				SI_setHealthPercentage(1 - %obj.getDamagePercent());
				SI_sethealthVisibility(true);
			}
			else
			{
				$StructuralInfinity::TagUI.setText("");
				SI_sethealthVisibility(false);
			}
		}
		else
		{
			$StructuralInfinity::TagUI.setText("");
			SI_sethealthVisibility(false);
		}
	}
	schedule(128, 0, SI_tagCast);
}

// build the pseudo tag display UI and position it over the same location as regular tags
function SI_initPseudoTagUI()
{
	if (isObject($StructuralInfinity::TagUI))
	{
		$StructuralInfinity::TagUI.delete();
	}

	%extent = PlayGui.getExtent();

	$StructuralInfinity::TagUI = new GuiMLTextCtrl()
	{
		allowColorChars = 0;
		bypassHideCursor = 0;
		deniedSound = "InputDeniedSound";
		extent = (getWord(%extent, 0) / 2) SPC "14";
		helpTag = "0";
		hideCursor = "0";
	};
	PlayGui.add($StructuralInfinity::TagUI);
	$StructuralInfinity::TagUI.setVisible(true);
	$StructuralInfinity::TagUI.setPosition((getWord(%extent, 0) / 2) + 24, (getWord(%extent, 1) / 2) + 24);
}

function SI_initHealthUI()
{
	%extent = PlayGUI.getExtent();

	if (isObject($StructuralInfinity::HealthUI))
		$StructuralInfinity::HealthUI.delete();
	if ($StructuralInfinity::HealthTeam $= "")
		return; // no health UI on older servers
	$StructuralInfinity::HealthUI = new HudBitmapCtrl()
	{
		extent = "48 10";
		fillColor = $StructuralInfinity::HealthTeam;
		frameColor = $StructuralInfinity::HealthTeam;
		opacity = 0.55;
	};
	PlayGUI.add($StructuralInfinity::HealthUI);
	$StructuralInfinity::HealthUI.setVisible(true);

	$StructuralInfinity::HealthUI.setPosition((getWord(%extent, 0) / 2) + 25, (getWord(%extent, 1) / 2) + 41);

	// frame ugliness
	if (isObject($StructuralInfinity::HealthUIFrameTop))
		$StructuralInfinity::HealthUIFrameTop.delete();
	$StructuralInfinity::HealthUIFrameTop = new HudBitmapCtrl()
	{
		fillColor = "0 0 0 0";
		frameColor = "1 1 1 0";
		opacity = 0.55 / 2;
	};
	PlayGUI.add($StructuralInfinity::HealthUIFrameTop);
	$StructuralInfinity::HealthUIFrameTop.setVisible(true);
	$StructuralInfinity::HealthUIFrameTop.setPosition((getWord(%extent, 0) / 2) + 24, (getWord(%extent, 1) / 2) + 40);
	$StructuralInfinity::HealthUIFrameTop.extent = "50 1";

	if (isObject($StructuralInfinity::HealthUIFrameBottom))
		$StructuralInfinity::HealthUIFrameBottom.delete();
	$StructuralInfinity::HealthUIFrameBottom = new HudBitmapCtrl()
	{
		fillColor = "0 0 0 0";
		frameColor = "1 1 1 0";
		opacity = 0.55 / 2;
	};
	PlayGUI.add($StructuralInfinity::HealthUIFrameBottom);
	$StructuralInfinity::HealthUIFrameBottom.setVisible(true);
	$StructuralInfinity::HealthUIFrameBottom.setPosition((getWord(%extent, 0) / 2) + 24, (getWord(%extent, 1) / 2) + 51);
	$StructuralInfinity::HealthUIFrameBottom.extent = "50 1";

	if (isObject($StructuralInfinity::HealthUIFrameLeft))
		$StructuralInfinity::HealthUIFrameLeft.delete();
	$StructuralInfinity::HealthUIFrameLeft = new HudBitmapCtrl()
	{
		fillColor = "0 0 0 0";
		frameColor = "1 1 1 0";
		opacity = 0.55 / 2;
	};
	PlayGUI.add($StructuralInfinity::HealthUIFrameLeft);
	$StructuralInfinity::HealthUIFrameLeft.setVisible(true);
	$StructuralInfinity::HealthUIFrameLeft.setPosition((getWord(%extent, 0) / 2) + 24, (getWord(%extent, 1) / 2) + 40);
	$StructuralInfinity::HealthUIFrameLeft.extent = "1 12";

	if (isObject($StructuralInfinity::HealthUIFrameRight))
		$StructuralInfinity::HealthUIFrameRight.delete();
	$StructuralInfinity::HealthUIFrameRight = new HudBitmapCtrl()
	{
		fillColor = "0 0 0 0";
		frameColor = "1 1 1 0";
		opacity = 0.55 / 2;
	};
	PlayGUI.add($StructuralInfinity::HealthUIFrameRight);
	$StructuralInfinity::HealthUIFrameRight.setVisible(true);
	$StructuralInfinity::HealthUIFrameRight.setPosition((getWord(%extent, 0) / 2) + 73, (getWord(%extent, 1) / 2) + 40);
	$StructuralInfinity::HealthUIFrameRight.extent = "1 12";
}

function SI_sethealthVisibility(%bool)
{
	if (isObject($StructuralInfinity::HealthUI))
	{
		$StructuralInfinity::HealthUI.setVisible(%bool);
		$StructuralInfinity::HealthUIFrameTop.setVisible(%bool);
		$StructuralInfinity::HealthUIFrameBottom.setVisible(%bool);
		$StructuralInfinity::HealthUIFrameLeft.setVisible(%bool);
		$StructuralInfinity::HealthUIFrameRight.setVisible(%bool);
	}
}

function SI_setHealthPercentage(%percent)
{
	if (isObject($StructuralInfinity::HealthUI))
		$StructuralInfinity::HealthUI.extent = mFloor(%percent * 48) SPC 10;
}

package StructuralInfinityClient
{
	// cleanup any client virtual ghosts when leaving a server
	// not doing this results in UE with very high probability
	function CreateServer(%mission, %missionType) {
		if (isActivePackage(StructuralInfinityClient)) {
			SI_doMemUnPatch();
			deactivatePackage(StructuralInfinityClient);
		}
		parent::CreateServer(%mission, %missionType);
		if (!isActivePackage(t2csri_server)) exec("t2csri/serverGlue.cs");
	}
	function clientCmdMissionEnd(%seq)
	{
		if (isObject(SIPieces))
		{
			while (SIPieces.getCount() > 0)
			{
				SIPieces.getObject(0).delete();
			}
		}
		Parent::clientCmdMissionEnd(%seq);
	}
	function DisconnectedCleanup()
	{
		if (isObject(SIPieces))
		{
			while (SIPieces.getCount() > 0)
			{
				SIPieces.getObject(0).delete();
			}
			SIPieces.delete();
		}
		deleteVariables("$StructuralInfinity::SpatialHashMap*");
		deleteVariables("$StructuralInfinity::ClientIndexMap*");
		$StructuralInfinity::Status::Active = 0;
		$StructuralInfinity::Status::PayloadCompression = 0;

		// call parent AFTER cleanup up SI
		Parent::DisconnectedCleanup();
	}

	// linker only implemented isClientGhost for static shapes, and even then only in some executables.
	// these used to check for group presence, but that didn't seem super reliable.
	// it attempts a variable access right now which should fail on ghosts
	function StaticShape::isClientGhost(%obj)
	{
		return (%obj.position $= "");
		//return (%obj.getGroup() == ServerConnection.getID());
	}
	function ForceFieldBare::isClientGhost(%obj)
	{
		return (%obj.position $= "");
		//return (%obj.getGroup() == ServerConnection.getID());
	}
	function Item::isClientGhost(%obj)
	{
		return (%obj.position $= "");
		//return (%obj.getGroup() == ServerConnection.getID());
	}

	// forcefields are never cloaked... this stops some console spam
	function ForceFieldBare::isCloaked(%obj)
	{
		return 0;
	}

	// process a notification from the server on server generated ghosts
	// moved to this package to address the missing tag bug if loaded in server mode
	function GameBaseData::onAdd(%data, %obj)
	{
		if (%obj.isClientGhost())
		{
			//echo(%obj.getTransform() SPC %obj.getScale() SPC %obj.getDatablock());
			SI_insertToIndex(%obj); // do spatial index processing
		}
	}
};
if ($Game::argv1 !$= "-dedicated" && !isObject(ServerGroup) && !isActivePackage(StructuralInfinityClient))
{
	activatePackage(StructuralInfinityClient);
	SI_doMemPatch(); // patch memory	
	runClientUpdateCheck($MiniClV);
}


echo("#EOF");
