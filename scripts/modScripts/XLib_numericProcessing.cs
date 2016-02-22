//------------------------------------------------------------------------------
// Numeric Processing (C) 2010 Robert MacGregor (AKA Dark Dragon DX)
//------------------------------------------------------------------------------

function getRandomVector(%min,%max)
{
 return getRandom(%min,%max) SPC getRandom(%min,%max) SPC getRandom(%min,%max);
}

function getRandomVectorAt(%vec,%min,%max)
{
 return vectorAdd(%vec,getRandomVector(%min,%max));
}

function decimalToPercent(%decimal){ return %decimal * 100; }
function percentToDecimal(%percent){ return %percent / 100; }

