$(document).ready(function () {

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
            console.log(item.productName);
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


    //Event onchange dropdown page order
    $('#selectedTable').change(function () {
        let res = this.value;
        window.location.replace('/Order/Index?tableID=' + res);
    });


    //Format money
    caculatePrice();

    function caculatePrice() {
        let price = 0;
        $("#table-pay-details").find("td").each(function () {
            let inputVal = $(this).data("values");
            if ($.isNumeric(inputVal)) {
                price += parseFloat(inputVal);
                $(this).find('.price-product').text(parseFloat(inputVal * 1000).toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,"));
            }
        });
        //price *= 1000;
        //$("#totalPricePay").text("Tổng tiền: " + price.toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,"));
        price = (price * 1000).toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,");
        $("#totalPricePay").text("Tổng tiền: " + price);
        $('#invoice-price').text(price);
    }

});