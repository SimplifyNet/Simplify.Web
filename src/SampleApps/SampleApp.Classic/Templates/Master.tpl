<!DOCTYPE html>
<html>

<head>
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>{Title}</title>
	<link rel="stylesheet" type="text/css" href="{~}/Styles/Main.min.css" />

	<link rel="shortcut icon" href="{~}/Images/favicon.ico" type="image/x-icon" />
</head>

<body>
	<script type="text/javascript" src="{~}/node_modules/jquery/dist/jquery.min.js"></script>
	<script type="text/javascript" src="{~}/node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

	<div class="Title">
		<img class="Logo" src="{~}/Images/IconMedium.png" alt="Simplify.Web" />Your Simplify.Web application
	</div>

	{Navbar}

	{MainContent}

	<div class="ExecutionTimeFooter">{LabelExecutionTime}: {SV:SiteExecutionTime}</div>
</body>

</html>