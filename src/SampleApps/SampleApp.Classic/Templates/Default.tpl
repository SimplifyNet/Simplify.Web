<div class="text-center">
	Hello from Kestrel-based console application with backend HTML generation, localization, authentication!
</div>

<div class="container-fluid">
	<div class="row">
		<div class="col-sm-6">
			<div class="panel-heading">
				<b>Links</b>
			</div>
			<ul class="list-group">
				<li class="list-group-item">
					<a href="https://github.com/SimplifyNet/Simplify.Web/wiki">Wiki</a>
				</li>
				<li class="list-group-item">
					<a href="https://github.com/SimplifyNet/Simplify.Web/tree/master/src">Source code</a>
				</li>
				<li class="list-group-item">
					<a href="https://github.com/SimplifyNet/Simplify.Web/tree/master/src/SampleApps">Example
						applications</a>
				</li>
				<li class="list-group-item">
					<a href="https://www.nuget.org/packages?q=Simplify.Web">Nuget package and extensions</a>
				</li>
			</ul>
		</div>
		<div class="col-sm-6">
			<div class="panel-heading">
				<b>Site variables</b>
			</div>
			<ul class="list-group">
				<li class="list-group-item">Site url: {SV:SiteUrl}</li>
				<li class="list-group-item">Site virtual path: {~}</li>
				<li class="list-group-item">
					Templates location: {SV:TemplatesDir}
				</li>
				<li class="list-group-item">Style: {SV:Style}</li>
				<li class="list-group-item">Language: {SV:Language}</li>
				<li class="list-group-item">
					Language with extension: {SV:LanguageExt}
				</li>
				<li class="list-group-item">
					Language culture name: {SV:LanguageCultureName}
				</li>
				<li class="list-group-item">
					Language culture name with extension:
					{SV:LanguageCultureNameExt}
				</li>
			</ul>
		</div>
	</div>
</div>