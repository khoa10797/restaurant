$(document).ready(function () {

    init();

    //Hub

    var hub = $.connection.restaurantHub;

    hub.client.updateWaittingFood = function (listInvoiceWatting) {
        var html = `<table class="table">
                        <thead>
                            <tr>
                                <th>Số bàn</th>
                                <th>Tên món</th>
                                <th>Số lượng</th>
                            </tr>
                        </thead>
                        <tbody>`;

        listInvoiceWatting.forEach(item => {
            html += `<tr>
                        <td>${item.tableID}</td>
                        <td>${item.productName}</td>
                        <td>${item.quantity}</td>
                        <td><a href="/Kitchen/Complete?invoiceDetailsID=${item.id}" class="glyphicon glyphicon-ok"></a></td>
                    </tr>`;
        });

        html += `   </tbody>
                </table>`;

        $('#table-kitchen').html(html);
    };

    $.connection.hub.start().done(function () {

        //Add new order

        $('.selecte-table-in-cart').click(function () {
            var tableID = $(this).data('values');
            var listCart = [];

            function Cart(productID, productName, quantity) {
                this.productID = productID;
                this.productName = productName;
                this.quantity = quantity;
            }

            $('.row-data-cart').each(function () {
                console.log(this);
                var productID = $(this).data('productid');
                var productName = $(this).data('productname');
                var quantity = $(this).data('quantity');
                listCart.push(new Cart(productID, productName, quantity));
            });

            hub.server.addNewOrder(tableID, listCart).done(function () {
                window.location.replace('/Cart/RemoveSession');
            });
        });

        //Remove order

        $('.remove-order').click(function () {
            let invoiceDetailsId = $(this).data('id');
            let tableId = $(this).data('values');
            hub.server.removeOrder(invoiceDetailsId).done(function () {
                window.location.replace('/Order/Index?tableID=' + tableId);
            });
        });

    });

    function init() {
        caculatePrice();
        formatMoneyForPayOrder();
        caculatePriceInvoiceDetail();
    }


    //Event onchange dropdown page order

    $('#selectedTable').change(function () {
        let res = this.value;
        window.location.replace('/Order/Index?tableID=' + res);
    });


    //Format money for page Pay Details

    function caculatePrice() {
        let price = 0;
        $("#table-pay-details").find("tr").each(function () {

            $(this).find('td').each(function () {
                let priceProduct = $(this).data('price');
                let inputVal = $(this).data('values');

                $(this).find('.price-product').text(parseFloat(priceProduct * 1000).toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,"));
                if ($.isNumeric(inputVal)) {
                    price += parseFloat(inputVal);
                    $(this).find('.total-price-product').text(parseFloat(inputVal * 1000).toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,"));
                }
            });

        });
        price = (price * 1000).toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,");
        $("#totalPricePay").text("Tổng tiền: " + price + " VNĐ");
        $('#invoice-price').text(price);
    }

    //Format money for page Order

    function formatMoneyForPayOrder() {
        $('#tableOrderFood').find('.price-product-order').each(function () {
            $(this).find('.span-price-product-order').text(parseFloat($(this).data('values') * 1000).toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,"));
        });

        $('#tableOrderFood').find('.total-price-order').each(function () {
            $(this).find('.span-total-price-order').text(parseFloat($(this).data('values') * 1000).toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,"));
        });
    }

    //Remove class selected-table for table

    $('#btnCloseModalPay').click(function () {
        $('#modalAddTable').find('.selected-table').each(function () {
            $(this).removeClass('selected-table');
        });
    });


    //Add invoice

    $('#btn-add-invoice').click(function () {

        var customerName = $('#customerName').val();
        var customerPhone = $('#customerPhone').val();
        var listTable = [];
        $('#modalAddTable').find('.selected-table').each(function () {
            var tableId = $(this).find('span').data('values');
            listTable.push(tableId);
        });

        if (listTable.length != 0) {
            $.ajax({
                url: "/Invoice/AddInvoice/",
                type: "POST",
                data: JSON.stringify({
                    "customerName": customerName,
                    "customerPhone": customerPhone
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var invoiceId = result.id;

                    $.ajax({
                        url: "/Invoice/AddTableForInvoice/",
                        type: "POST",
                        data: JSON.stringify({
                            "invoiceId": invoiceId,
                            "listTable": listTable
                        }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function () {

                        }
                    }).always(function () {
                        window.location.replace('/Invoice/Index');
                    });
                },
                error: function (err) {
                    console.log(err);
                }
            });
        } else {
            alert('Bạn chưa chọn bàn!');
        }
    });

    //Add or remove product in cart
    $('.port-info').click(function () {
        var _this = this;
        var action = $(this).data('action');
        var productId = $(this).data('id');
        if (action == 'select') {
            $.ajax({
                url: "/Cart/AddCart/",
                type: "POST",
                data: { productId: productId },
                success: function () {
                    $(_this).find('.fas').addClass('fa-check-circle');
                    $(_this).data('action', 'remove');
                }
            });
        }
        else {
            $.ajax({
                url: "/Cart/RemoveCart/",
                type: "POST",
                data: { productId: productId },
                success: function () {
                    $(_this).find('.fas').removeClass('fa-check-circle');
                    $(_this).data('action', 'select');
                }
            });
        }
    });

    //Remove product in cart

    $('button[data-role="remove"]').click(function () {
        var productId = $(this).data('id');
        $.ajax({
            url: "/Cart/RemoveCart/",
            type: "POST",
            data: { productId: productId },
            success: function () {
                var rowId = '#' + productId;
                $(rowId).remove();
            }
        });
    });

    //Event change quantity product of input page cart

    $('.input-quantity-cart').change(function () {
        var productId = $(this).data('id');
        var quantity = $(this).val();
        $.ajax({
            url: "/Cart/UpdateCart/",
            type: "POST",
            data: { productId: productId, quantity: quantity },
            success: function () {
                var rowId = '#' + productId;
                $(rowId).data('quantity', quantity);
            }
        });
    });

    //Format birthday for page user in area admin
    $('.birthday').each(function () {
        var s = $(this).data('birthday').split(' ');
        var day = s[0];
        $(this).text(day);
    });

    //Format sex for page user in area admin
    $('.user-sex').each(function () {
        var s = $(this).data('sex');
        var sex;
        if (s == 'True')
            sex = 'Nam';
        else
            sex = 'Nữ';
        $(this).text(sex);
    });

    //Remove user
    $('.btn-remove-user').click(function () {
        var userId = $(this).data('id');
        $.ajax({
            url: "/User/RemoveUser/",
            type: "POST",
            data: { id: userId },
            success: function () {
                var rowId = '#u' + userId;
                $(rowId).remove();
            },
            error: function () {
                alert("Bạn không được phép xóa người dùng!")
            }
        });
    });

    //Remove product
    $('.btn-remove-product').click(function () {
        var productId = $(this).data('id');
        $.ajax({
            url: "/Product/RemoveProduct/",
            type: "POST",
            data: { id: productId },
            success: function () {
                var rowId = '#p' + productId;
                $(rowId).remove();
            },
            error: function () {
                alert("Bạn không được phép xóa món ăn!")
            }
        });
    });

    // Format status in table invoice
    $('.status-invoice').each(function () {
        var status = $(this).data('status');
        if (status == 'True')
            $(this).text('Chưa thanh toán')
        else
            $(this).text('Đã thanh toán')
    });

    // Format date created in table invoice
    $('.format-date').each(function () {
        var s = $(this).data('date').split(' ');
        var day = s[0];
        $(this).text(day);
    });

    // Format invoice detail

    function caculatePriceInvoiceDetail() {
        let price = 0;
        $(".row-invoice-detail").each(function () {

            $(this).find('td').each(function () {
                let priceProduct = $(this).data('price');
                let inputVal = $(this).data('values');

                $(this).find('.price-product').text(parseFloat(priceProduct * 1000).toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,"));
                if ($.isNumeric(inputVal)) {
                    price += parseFloat(inputVal);
                    $(this).find('.total-price-product').text(parseFloat(inputVal * 1000).toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,"));
                }
            });

        });
        price = (price * 1000).toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,");
        $("#total-price-invoice").text("Tổng tiền: " + price + " VNĐ");
    }

    // Event change datetime picker
    $('#datetime-picker').change(function () {
        var date = $(this).val();
        window.location.replace("/Admin/Report/FindInvoice?date=" + date);
    });
});
