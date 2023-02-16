jQuery(document).ready(function () {

	$('#update-user-info').on('click', function () {
		let firstName = $('#first-name').val();
		let secondName = $('#second-name').val();
		let jwt = Cookies.get('jwt');
		$.ajax({
			url: 'https://localhost:7208/api/users/profile',
			type: 'PUT',
			headers: {
				'Content-Type': 'application/json',
				'Authorization': `Bearer ${jwt}`
			},
			data: JSON.stringify({ firstName: firstName, lastName: secondName }),
			dataType: 'json',
			success: function (response) {
				alert("success");
			}
		});
	});

});