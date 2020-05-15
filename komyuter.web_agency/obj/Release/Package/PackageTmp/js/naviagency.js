
$(function () {

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
			"agency_id": {
				required: true,
				maxlength: 11
			},
			"agency_name": {
				required: true,
				maxlength: 255
			},
			"agency_url": {
				required: true,
				maxlength: 255
			},
			"agency_timezone": {
				required: true,
				maxlength: 50
			},
			"agency_lang": {
				maxlength: 2
			},
			"agency_phone": {
				maxlength: 50
			},

			"service_id": {
				required: true,
				maxlength: 35
			},
			"start_date": {
				required: true,
				maxlength: 8
			},
			"end_date": {
				required: true,
				maxlength: 8
			},

			"trip_id": {
				required: true,
				maxlength: 35
			},
			"route_id": {
				required: true,
				maxlength: 35
			},
			"trip_headsign": {
				required: true,
				maxlength: 255
			},
			"trip_short_name": {
				required: true,
				maxlength: 50
			},
			"shape_id": {
				required: true,
				maxlength: 35
			},
			"direction_id": {
				maxlength: 1
			},

			"start_time": {
				required: true,
				maxlength: 8
			},
			"end_time": {
				required: true,
				maxlength: 8
			},

			"headway_secs": {
				required: true,
				maxlength: 8
			},
			"exact_times": {
				required: true,
				maxlength: 8
			},

			"arrival_time": {
				required: true,
				maxlength: 8
			},
			"departure_time": {
				required: true,
				maxlength: 8
			},
			"stop_sequence": {
				required: true,
				maxlength: 2
			},
			"stop_id": {
				required: true,
				maxlength: 20
			},
			"stop_name": {
				required: true,
				maxlength: 255
			},
			"shape_pt_sequence": {
				required: true,
				maxlength: 2
			},
			"mobile_number": {
				required: true,
				maxlength: 25
			},
			"access_token": {
				required: true,
				maxlength: 255
			},

			"header": {
				required: true,
				maxlength: 255
			},
			"description": {
				required: true,
				maxlength: 500
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