<!DOCTYPE html>
<html>

<head>
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>{Title}</title>
	<link rel="stylesheet" type="text/css" href="{~}/content/bootstrap.min.css" />
	<link rel="stylesheet" type="text/css" href="{~}/content/bootstrap-theme.min.css" />
	<link rel="stylesheet" type="text/css" href="{~}/styles/Main.min.css" />
</head>

<body>
	<script type="text/javascript" src="{~}/scripts/jquery-3.1.0.min.js"></script>
	<script type="text/javascript" src="{~}/scripts/bootstrap.min.js"></script>

	<div class="Title">
		<img class="Logo" src="{~}/images/IconMedium.png" alt="Simplify.Web" />Your Simplify.Web application
	</div>

	{Navbar}

	{MainContent}

	<div class="ExecutionTimeFooter">{LabelExecutionTime}: {SV:SiteExecutionTime}</div>
</body>

</html>