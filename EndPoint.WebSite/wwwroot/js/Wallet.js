function DepositMoney(walletId) {
        Swal.fire({
            icon: "question",
            title: 'واریز به کیف',
            text: "لطفا مبلغ مورد نظر را وارد کنید",
            input: "text",
            inputValue: "",
            inputAttributes: {
                autocapitalize: "off"
            },
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'واریز',
            cancelButtonText: 'انصراف',
            showLoaderOnConfirm: true,
            closeOnConfirm: true,
            closeOnCancel: true,
            preConfirm: async (amount) => {
                var postData = {
                    'WalletId': walletId,
                    'Amount': amount
                };

                $.ajax({
                    contentType: 'application/x-www-form-urlencoded',
                    dataType: 'json',
                    type: "POST",
                    url: "/Admin/Wallets/DepositMoney",
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
            },
            allowOutsideClick: () => !Swal.isLoading()
        });
}

function WithdrawMoney(walletId) {
    Swal.fire({
        icon: "question",
        title: 'برداشت از کیف',
        text: "لطفا مبلغ مورد نظر را وارد کنید",
        input: "text",
        inputValue: "",
        inputAttributes: {
            autocapitalize: "off"
        },
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'برداشت',
        cancelButtonText: 'انصراف',
        showLoaderOnConfirm: true,
        closeOnConfirm: true,
        closeOnCancel: true,
        preConfirm: async (amount) => {
            var postData = {
                'WalletId': walletId,
                'Amount': amount
            };

            $.ajax({
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json',
                type: "POST",
                url: "/Admin/Wallets/WithdrawMoney",
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
        },
        allowOutsideClick: () => !Swal.isLoading()
    });
}