
$("#btnAddFeatures").on("click", function () {

    var txtDisplayName = $("#txtDisplayName").val();
    var txtValue = $("#txtValue").val();

    if (txtDisplayName == "" || txtValue == "") {
        swal.fire(
            'هشدار!',
            "نام و مقدار را باید وارد کنید",
            'warning'
        );
    }
    else {
        $('#tbl_Features tbody').append('<tr> <td>' + txtDisplayName + '</td>  <td>' + txtValue + '</td> <td> <a class="idFeatures btnDelete btn btn-danger"> حذف </a> </td> </tr>');
        $("#txtDisplayName").val('');
        $("#txtValue").val('');
    }
});

$("#tbl_Features").on('click', '.idFeatures', function () {
    $(this).closest('tr').remove();
});



$('#btnAddProduct').on('click', function () {

    var data = new FormData();

    //دریافت مقادیر از تکس باکس ها و....
    data.append("Name", $("#Name").val());
    data.append("Brand", $("#Brand").val());
    data.append("Price", $("#Price").val());
    data.append("Inventory", $("#Inventory").val());
    data.append("Displayed", $("#Displayed").attr("checked") ? true : false);
    data.append("CategoryId", $('#Category').find('option:selected').val());
    data.append("Description", $("#Description").val());


    //دریافت عکس های انتخاب شده توسط کاربر و قرار دادن عکس ها در متغیر data
    var productImages = document.getElementById("Images");

    if (productImages.files.length > 0) {
        for (var i = 0; i < productImages.files.length; i++) {
            data.append('ProductImages-' + i, productImages.files[i]);
        }
    }

    //دریافت ویژگی های محصول از جدول
    var dataFeaturesViewModel = $('#tbl_Features tr:gt(0)').map(function () {
        return {
            DisplayName: $(this.cells[0]).text(),
            Value: $(this.cells[1]).text(),
        };
    }).get();

    $.each(dataFeaturesViewModel, function (i, val) {

        //data.append("[" + i + "]ProductFeatures", {
        //    'Name': val.DisplayName,
        //    'Value': val.Value,
        //});
        data.append('ProductFeatures[' + i + '].Name', val.DisplayName);
        data.append('ProductFeatures[' + i + '].Value', val.Value);

    });

    // ارسال اطلاعات بع کنترلر
    var ajaxRequest = $.ajax({
        type: "POST",
        url: "Create",
        contentType: false,
        processData: false,
        data: data,
        success: function (data) {

            if (data.isSuccess == true) {

                swal.fire(
                    'موفق!',
                    data.message,
                    'success'
                ).then(function (isConfirm) {
                    window.location.href = "/Admin/Products/";

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
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }

    });

    ajaxRequest.done(function (xhr, textStatus) {
        // Do other operation
    });
});

function DeleteProduct(Id) {
    swal.fire({
        title: 'حذف محصول',
        text: "آیا از حذف این محصول آن اطمینان دارید؟",
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
                url: "~/Admin/Products/Delete/",
                data: postData,
                success: function (data) {
                    if (data.isSuccess == true) {
                        swal.fire(
                            'موفق!',
                            data.message,
                            'success'
                        ).then(function (isConfirm) {
                            window.location.replace("~/Admin/Products/");
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