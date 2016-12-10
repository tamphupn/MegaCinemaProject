//Toggle event 
function toggleClick()
{
	var x = document.getElementById("topbar-nav");
	if (x.className === "clearfix")
	{
		x.className+=" responsive-topbar";
	}
	else
	{
		x.className="clearfix";
	}
}

//Detect screen resolution 
$(window).resize(function()
{
	var width = screen.width;
		height = screen.height;
	if (width>=900)
	{
		var x = document.getElementById("topbar-nav");
		if (x.className === "clearfix")
		{
			
		}
		else
		{
			toggleClick();
		}
	}	
});

