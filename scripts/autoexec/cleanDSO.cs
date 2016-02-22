// #autoload
// #name = cleanDSO
// #version = 1.0
// #date = December 21st, 2001
// #author = Paul Tousignant
// #warrior = UberGuy (FT)
// #email = uberguy@skyreach.cas.nwu.edu
// #web = http://scripts.tribalwar.com/uberguy
// #description = Remove all your *.dso files every time you exit T2.
// #status = beta

package noDso {
	function quit() {
    		%cnt = 0;
		%tmpObj = new ScriptObject() {};
		for(%file = findFirstFile("*.dso"); %file !$= ""; %file = findNextFile("*.dso")) {
			%tmpObj.file[%cnt++] = %file;
		}
		for (%i=0; %i<%cnt; %i++) {
			deleteFile(%tmpObj.file[%i]);
		}
		%tmpObj.delete();
 
    return parent::quit();
    }
};
activatePackage(noDso);

