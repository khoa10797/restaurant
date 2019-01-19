$(document).ready(function () {

    //Hub

    var hub = $.connection.restaurantHub;

    hub.client.addNewOrderForKitChen = function (listInvoiceWatting) {
        var html = `<table class="table">
                        <thead>
                            <tr>
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
        $('#btn-submit-order').click(function () {
            hub.server.addNewOrder();
        });
    });


    //Event onchange dropdown page order
    $("#tableID").change(function () {
        let res = this.value;
        window.location.replace("/Order/Index?tableID=" + res);
    });

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
        price *= 1000;
        $("#totalPricePay").text("Tổng tiền: " + price.toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, "$1,"));
    }

});