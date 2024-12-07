function PayUsingCompanyWallet() {
    swal.fire({
        title: 'پرداخت',
        text: "آیا از پرداخت به وسیله ی کیف پول شرکتی اطمینان دارید؟",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'تایید',
        cancelButtonText: 'انصراف',
    }).then((result) => {
        if (result.value) {
            $.ajax({
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json',
                type: "POST",
                url: "/Pay/PayByCompanyWallet",
                success: function (data) {
                    if (data.isSuccess == true) {
                        swal.fire(
                            'موفق!',
                            data.message,
                            'success'
                        ).then(function (isConfirm) {
                            window.location.replace("/Orders");
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

function PayUsingPersonalWallet() {
    swal.fire({
        title: 'پرداخت',
        text: "آیا از پرداخت به وسیله ی کیف پول شخصی اطمینان دارید؟",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'تایید',
        cancelButtonText: 'انصراف',
    }).then((result) => {
        if (result.value) {
            $.ajax({
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json',
                type: "POST",
                url: "/Pay/PayByPersonalWallet",
                success: function (data) {
                    if (data.isSuccess == true) {
                        swal.fire(
                            'موفق!',
                            data.message,
                            'success'
                        ).then(function (isConfirm) {
                            window.location.replace("/Orders");
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