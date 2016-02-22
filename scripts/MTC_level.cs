//Basic Levels;

//These make up the spawning behaviour of the mtc weapon types
//Only starting ideas for now
//Changes are culmative

//Only targeting lasers
function MTCLevel0()
{
$MTCCHANGE::weapon[0] = "target";
$MTCCHANGE::change[0] = 1;
$MTCCHANGE::totchange = 1;
$MTCCHANGE::count = 1;
}

//9 targeting lasers
//1 blaster
function MTCLevel1()
{
$MTCCHANGE::weapon[0] = "target";
$MTCCHANGE::change[0] = 9;
$MTCCHANGE::weapon[0] = "blaster";
$MTCCHANGE::change[0] = 10;
$MTCCHANGE::totchange = 10;
$MTCCHANGE::count = 2;
}

//8 targeting lasers
//2 blasters
//1 grenades
function MTCLevel2()
{
$MTCCHANGE::weapon[0] = "target";
$MTCCHANGE::change[0] = 8;
$MTCCHANGE::weapon[0] = "blaster";
$MTCCHANGE::change[0] = 10;
$MTCCHANGE::weapon[0] = "grenade";
$MTCCHANGE::change[0] = 11;
$MTCCHANGE::totchange = 11;
$MTCCHANGE::count = 3;
}