
$(function () {

	$("select[name=\"route_id\"]").select2({
		allowClear: true,
		placeholder: "Choose a station . . . ",
	}).change(function () {
		$(this).valid();
	});

	$("select[name=\"route_id\"]").change(function () {
		$(this).find(":selected").each(function () {
			var ddValue = $(this).val();

			if (ddValue) {
				changeRoute(ddValue)
			}
		});
	});

	function changeRoute(id) {

		// populate dropdwon
		var $dropdown = $("select[name=\"stop_id\"]");
		var uri = "https://komyuterrealtime.azurewebsites.net/api/Stop?route_id=" + id;
		$.getJSON(uri)
			.done(function (data) {
				$dropdown
					.empty()
					.append('<option value>Choose a station...</option>');

				$.each(data, function () {

					$dropdown.append($("<option />").val(this.stop_id).text(this.stop_name));
				});
			});
	}

	// Datatables basic
	$('#datatables-basic').DataTable({
		responsive: true
	});
	// Datatables with Buttons
	var datatablesButtons = $('#datatables-buttons').DataTable({
		lengthChange: !1,
		buttons: ["copy", "print"],
		responsive: true
	});
	datatablesButtons.buttons().container().appendTo("#datatables-buttons_wrapper .col-md-6:eq(0)")

	$('#createnew').click(function () {
		console.log($(this).val())
		window.location.href = "/" + $(this).val();
	});

	// Trigger validation on tagsinput change
	$("input[name=\"validation-bs-tagsinput\"]").on("itemAdded itemRemoved", function () {
		$(this).valid();
	});
	$("#validation-form").validate({
		ignore: ".ignore, .select2-input",
		focusInvalid: false,
		rules: {

			"route_id": {
				required: true
			},
			//"stop_id": {
			//	required: true,
			//	maxlength: 20
			//},
			"header": {
				required: true,
				maxlength: 255
			},
			"description": {
				required: true,
				maxlength: 500
			},

			"input_start_date": {
				required: true
			},
			"end_time": {
				required: true
			},

			//"validation-email": {
			//	required: true,
			//	email: true
			//},
			//"validation-password": {
			//	required: true,
			//	minlength: 6,
			//	maxlength: 20
			//},
			//"validation-password-confirmation": {
			//	required: true,
			//	minlength: 6,
			//	equalTo: "input[name=\"validation-password\"]"
			//},
			"validation-required": {
				required: true
			}
			//"validation-url": {
			//	required: true,
			//	url: true
			//},
			//"validation-select": {
			//	required: true
			//},
			//"validation-multiselect": {
			//	required: true,
			//	minlength: 2
			//},
			//"validation-select2": {
			//	required: true
			//},
			//"validation-select2-multi": {
			//	required: true,
			//	minlength: 2
			//},
			//"validation-text": {
			//	required: true
			//},
			//"validation-file": {
			//	required: true
			//},
			//"validation-radios": {
			//	required: true
			//},
			//"validation-radios-custom": {
			//	required: true
			//},
			//"validation-checkbox": {
			//	required: true
			//},
			//"validation-checkbox-custom": {
			//	required: true
			//},
			//"validation-checkbox-group-1": {
			//	require_from_group: [1, "input[name=\"validation-checkbox-group-1\"], input[name=\"validation-checkbox-group-2\"]"]
			//},
			//"validation-checkbox-group-2": {
			//	require_from_group: [1, "input[name=\"validation-checkbox-group-1\"], input[name=\"validation-checkbox-group-2\"]"]
			//},
			//"validation-checkbox-custom-group-1": {
			//	require_from_group: [1, "input[name=\"validation-checkbox-custom-group-1\"], input[name=\"validation-checkbox-custom-group-2\"]"]
			//},
			//"validation-checkbox-custom-group-2": {
			//	require_from_group: [1, "input[name=\"validation-checkbox-custom-group-1\"], input[name=\"validation-checkbox-custom-group-2\"]"]
			//}
		},
		// Errors
		errorPlacement: function errorPlacement(error, element) {
			var $parent = $(element).parents(".col-sm-10");
			// Do not duplicate errors
			if ($parent.find(".jquery-validation-error").length) {
				return;
			}
			$parent.append(
				error.addClass("jquery-validation-error small form-text invalid-feedback")
			);
		},
		highlight: function (element) {
			var $el = $(element);
			var $parent = $el.parents(".col-sm-10");
			$el.addClass("is-invalid");
			// Select2 and Tagsinput
			if ($el.hasClass("select2-hidden-accessible") || $el.attr("data-role") === "tagsinput") {
				$el.parent().addClass("is-invalid");
			}
		},
		unhighlight: function (element) {
			$(element).parents(".col-sm-10").find(".is-invalid").removeClass("is-invalid");
		}
	});
});