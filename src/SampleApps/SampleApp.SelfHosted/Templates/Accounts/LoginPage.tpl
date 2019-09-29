<div class="container" style="max-width: 400px">
	<form class="form-horizontal" method="post" id="Form" data-bv-feedbackicons-valid="glyphicon glyphicon-ok"
		data-bv-feedbackicons-invalid="glyphicon glyphicon-remove" data-bv-feedbackicons-validating="glyphicon glyphicon-refresh"
		data-bv-submitbuttons='button[type="submit"]'>
		<div class="form-group">
			{Message}
		</div>
		<div class="form-group">
			<input type="text" class="form-control" name="UserName" placeholder="User name" value="{UserName}" required
				data-bv-notempty-message="{UserNameNotEmptyMessage}" />
		</div>
		<div class="form-group">
			<input type="Password" class="form-control" name="Password" placeholder="Password" required data-bv-notempty-message="{PasswordNotEmptyMessage}" />
		</div>
		<div class="form-group">
			<label><input type="checkbox" name="RememberMe" {RememberMe} /> Remember me</label>
		</div>
		<div class="form-group">
			<i>Note: Login: Foo, password: 1</i>
		</div>

		<div class="form-group">
			<button type="submit" class="btn btn-default">Login</button>
		</div>
	</form>
</div>