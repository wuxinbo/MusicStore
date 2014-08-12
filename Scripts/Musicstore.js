function init() {
    $('button,button,#button').button();
   
    $(document).tooltip();

}
function amount() {
    var value = $(this).val();
    if (value == '+') {
        
        $('#amount_value').val(parseInt($('#amount_value').val())+1); 
    } else if (value == '-' && $('#amount_value').val() > 1) {
        $('#amount_value').val(parseInt($('#amount_value').val()) - 1);
    }
}
function valie_amount() {
    var value = $('#amount_value').val();
    var is = isNaN(value);
    if (is) {
        $('#amount_value').val("1");
    }
}
//页面按钮css样式。
function page_choose() {
    var current_page = $('#current_page').val();
    var page_val = $("[href='/Storemannager?Page=1']").text();
    $('#page').children().each(function () {
        //alert($(this).text());
        var page=$(this).children();
        if (page.text() == current_page) {
            page.parent().addClass('current_page');
            page.addClass('current_page');
        } else {
            
        }
    });
     
    }
function liupai_position() {
    $("[href='/store']").addClass('liupai');
}
function addToCart() {
    var album_1 = $.cookie('albums');
    if (album_1 != null) {
        var object = JSON.parse(album_1);
        var albums = object;
    } else {
        var albums = new Array();
    }
    

    albums[albums.length] = new album();
    //JSON.stringify(albums);
    //将js对象转化成json字符串。
    $.cookie('albums', JSON.stringify(albums), {path:"/"});
    
    //alert(albums.length);
}
function album() {
    this.title = $('#biao_value').text();
    this.img = $('#img').attr('src');
    this.id = $('#biao_value').text();
    this.count = $('#amount_value').val();
    this.price = $('#price_value').text();
}
function parse_json_cart() {
    var album_1 = $.cookie('albums');
    //alert(album_1);
    var object = JSON.parse(album_1);
   
    for (var i in object) {
             
        var str = ".item_info_" + i;
        $(str).find(' #cart_img').attr('src', object[i].img);
        $(str).find(' #cart_title').text(object[i].title);
        $(str).find('#item_price').text(object[i].price);
        $(str).find('#cart_count').text(object[i].count);
       
       
    }
    $('#counts').text(object.length);
    $('#total').text('$'+sum(object));
    $('#p_length').val(object.length);
}
//计算商品的总价。
function sum(ob1) {
    var total = 0.0;
    var prices=new Array();
    for (var i in ob1) {
        var price = ob1[i].price;
        //去掉字符串中的空格
        price = price.substring(price.indexOf('$')+1, price.indexOf('.')+3);
       //单个物品的价格。
        var total_price = parseFloat(price) * parseInt(ob1[i].count);
        prices[i] = total_price;
    }
    for (var i in prices) {
        total = prices[i] + total;
    }
   
    return total;
}
