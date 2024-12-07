function Registeruser() {
    swal.fire({
        title: 'ثبت نام کاربر',
        text: "آیا میخواهید ثبت نام کاربر را انجام دهید؟",
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله ثبت نام انجام شود',
        cancelButtonText: 'خیر'
    }).then((result) => {
        if (result.value) {

            var fullname = $("#fullname").val();
            var email = $("#email").val();
            var RoleId = $("#RoleId").val();
            var Password = $("#Password").val();
            var RePassword = $("#RePassword").val();

            var postData = {
                'fullname': fullname,
                'email': email,
                'RoleId': RoleId,
                'Password': Password,
                'RePassword': RePassword,
            };

            $.ajax({
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json',
                type: "POST",
                url: "Create",
                data: postData,
                success: function (data) {
                    if (data.isSuccess == true) {
                        swal.fire(
                            'موفق!',
                            data.message,
                            'success'
                        )
                    }
                    else {
                        swal.fire(
                            'هشدار!',
                            data.message,
                            'warning'
                        );

                    }
                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }
            });
        }
    })
}

function DeleteUser(UserId) {
    swal.fire({
        title: 'حذف کاربر',
        text: "کاربر گرامی از حذف کاربر مطمئن هستید؟",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#7cacbe',
        confirmButtonText: 'بله ، کاربر حذف شود',
        cancelButtonText: 'خیر'
    }).then((result) => {
        if (result.value) {
            var postData = {
                'Id': UserId,
            };

            $.ajax({
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json',
                type: "POST",
                url: "Users/Delete",
                data: postData,
                success: function (data) {
                    if (data.isSuccess == true) {
                        swal.fire(
                            'موفق!',
                            data.message,
                            'success'
                        ).then(function (isConfirm) {
                            location.reload();
                        });
                    }
                    else {

                        swal.fire(
                            'هشدار!',
                            data.message,
                            'warning'
                        );

                    }
                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }

            });

        }
    })
}


function UserStatusChange(UserId) {
    swal.fire({
        title: 'تغییر وضعیت کاربر',
        text: "کاربر گرامی از تغییر وضعیت کاربر مطمئن هستید؟",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#7cacbe',
        confirmButtonText: 'بله ، تغییر وضعیت انجام شود',
        cancelButtonText: 'خیر'
    }).then((result) => {
        if (result.value) {

            var postData = {
                'Id': UserId,
            };

            $.ajax({
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json',
                type: "POST",
                url: "Users/UserStatusChange",
                data: postData,
                success: function (data) {
                    if (data.isSuccess == true) {
                        swal.fire(
                            'موفق!',
                            data.message,
                            'success'
                        ).then(function (isConfirm) {
                            location.reload();
                        });
                    }
                    else {

                        swal.fire(
                            'هشدار!',
                            data.message,
                            'warning'
                        );

                    }
                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }

            });

        }
    })
}


function Edituser() {

    var userId = $("#Edit_UserId").val();
    var fullName = $("#Edit_Fullname").val();

    var postData = {
        'Id': userId,
        'fullName': fullName,
    };


    $.ajax({
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        type: "POST",
        url: "Users/Edit",
        data: postData,
        success: function (data) {
            if (data.isSuccess == true) {
                swal.fire(
                    'موفق!',
                    data.message,
                    'success'
                ).then(function (isConfirm) {
                    location.reload();
                });
            }
            else {
                swal.fire(
                    'هشدار!',
                    data.message,
                    'warning'
                );
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });

    $('#EditUser').modal('hide');

}


function ShowModalEdituser(UserId, fullName) {
    $('#Edit_Fullname').val(fullName)
    $('#Edit_UserId').val(UserId)

    $('#EditUser').modal('show');

}
