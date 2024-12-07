function AddNewCategory() {
    swal.fire({
        title: 'دسته بندی جدید',
        text: "آیا میخواهید دسته بندی جدید به لیست دسته بندی ها اضافه شود؟",
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله، اضافه شود',
        cancelButtonText: 'خیر'
    }).then((result) => {
        if (result.value) {
            var Name = $("#Name").val();
            var parentId = $("#parentId").val();


            var postData = {
                'Name': Name,
                'ParentId': parentId,
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
                        ).then(function (isConfirm) {
                            window.location.replace("/Admin/Categories/");
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
                    swal.fire(
                        'هشدار!',
                        request.responseText,
                        'warning'
                    );
                }

            });

        }
    })
}
function DeleteCategory(Id) {
    swal.fire({
        title: 'حذف دسته بندی',
        text: "آیا از حذف این دسته بندی و تمام فرزندان آن اطمینان دارید؟",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#7cacbe',
        confirmButtonText: 'بله، حذف شود',
        cancelButtonText: 'خیر'
    }).then((result) => {
        if (result.value) {
            var postData = {
                'Id': Id
            };

            $.ajax({
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json',
                type: "POST",
                url: "/Admin/Categories/Delete",
                data: postData,
                success: function (data) {
                    if (data.isSuccess == true) {
                        swal.fire(
                            'موفق!',
                            data.message,
                            'success'
                        ).then(function (isConfirm) {
                            window.location.replace("/Admin/Categories/");
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
                    swal.fire(
                        'هشدار!',
                        request.responseText,
                        'warning'
                    );
                }

            });

        }
    })
}
function EditCategory(Id) {
    swal.fire({
        title: 'ویرایش دسته بندی',
        text: "آیا از ویرایش این دسته بندی اطمینان دارید؟",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#7cacbe',
        confirmButtonText: 'بله، حذف شود',
        cancelButtonText: 'خیر'
    }).then((result) => {
        if (result.value) {
            var name = $("#Name").val();
            var postData = {
                'Id': Id,
                'Name': name,
            };

            $.ajax({
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json',
                type: "POST",
                url: "/Admin/Categories/Edit",
                data: postData,
                success: function (data) {
                    if (data.isSuccess == true) {
                        swal.fire(
                            'موفق!',
                            data.message,
                            'success'
                        ).then(function (isConfirm) {
                            window.location.replace("/Admin/Categories/");
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
                    swal.fire(
                        'هشدار!',
                        request.responseText,
                        'warning'
                    );
                }

            });

        }
    })
}