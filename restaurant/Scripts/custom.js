$(document).ready(function () {

    ////Validate form page order
    //$("#form-order").validate({
    //    rules: {
    //        quantity: "required"
    //    },
    //    messages: {
    //        quantity: "Vui lòng chọn số lượng!"
    //    }
    //});

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
        console.log(price);
    }

});