//This function is based on a similar one by Lt. Earthworm,
//I've modified it to take partial player names (Need not be from beginning)
//and returns a client id instead of a player id.
function plnametocid(%name)
{
    %count = ClientGroup.getCount(); //counts total clients
    for(%i = 0; %i < %count; %i++)  //loops till all clients are accounted for
        {
        %obj = ClientGroup.getObject(%i);  //gets the clientid based on the ordering hes in on the list
        %nametest=%obj.namebase;  //pointless step but i didnt feel like removing it....
        %nametest=strlwr(%nametest);  //make name lowercase
        %name=strlwr(%name);  //same as above, for the other name
        if(strstr(%nametest,%name) != -1)  //is all of name test used in name
            return %obj;  //if so return the clientid and stop the function
    }
    return 0;  //if none fits return 0 and end function
}

