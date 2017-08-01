$(function () {
    $("button[data-add-to-cart]").click(function () {
        var pid = $(this).attr("data-add-to-cart");
        $.ajax({
            url: "/ShoppingCart/Add",
            data: { id: pid },
            type: "post",
            success: function (response) {
                $(".nn-cart #count").html(response.Count);
                $(".nn-cart #amount").html(response.Amount);
            }
        });

        /*
        * Hiệu ứng bay vào giỏ hàng
        */
        var img = $(this).parents(".panel").find(".panel-body img");
        var css = ".cart-fly{background-image:url('" + img.attr("src") + "');background-size:100% 100%}";
        $("#cart-fly").html(css);
        img.effect("transfer", { to: ".nn-cart img", className: "cart-fly" }, 1000);
    });


    $("button[data-remove-from-cart]").click(function () {
        var pid = $(this).attr("data-remove-from-cart");
        $.ajax({
            url: "/Shoppingcart/Remove",
            data: { id: pid },
            type: "post",
            success: function (response) {
                $(".nn-cart #count").html(response.Count);
                $(".nn-cart #amount").html(response.Amount);
            }
        });
        /*
        *Hiệu ứng xóa <tr>
        */
        $(this).parents("tr").hide(500);
    });

    $("input[data-update-cart]").change(function () {
        var pid = $(this).attr("data-update-cart");
        var qty = $(this).val();
        var tr = $(this).parents("tr");
        $.ajax({
            url: "/ShoppingCart/Update",
            data: { id: pid, newqty: qty },
            type: "post",
            success: function (response) {
                $(".nn-cart #count").html(response.Count);
                $(".nn-cart #amount").html(response.Amount);

                //cập nhật thành tiền của sản phẩm
                tr.find("td:eq(4)").html(response.ItemAmount);
            }
        });
    });
})