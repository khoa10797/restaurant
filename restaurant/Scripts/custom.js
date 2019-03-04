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

        $('#btn-submit-order').click(function () {
            let tableId = $('#tableID').val();
            let invoiceId = $('#invoiceID').val();
            let productId = $('#productID').val();
            let quantity = $('#quantity-order').val();
            hub.server.addNewOrder(tableId, invoiceId, productId, quantity).done(function () {
                window.location.replace('/Order/Index?tableID=' + tableId);
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


});