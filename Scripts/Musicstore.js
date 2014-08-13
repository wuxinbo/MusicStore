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
    
    if (album_1 != null && album_1.length>=1) {
        var object = JSON.parse(album_1);
        var albums = object;
    } else {
        var albums = new Array();
    }
    //实例化一个对象
    var album_2 = new album();
    var id = album_2.id;
    if (albums.length > 0) {
        for (var i in albums) {
            //如果ID相同则说明是同一个产品。
            if (id == albums[i].id) {
                albums[i].count = album_2.count;
            } else {
                albums[albums.length] = album_2;
            }
        }
    } else {
        //if(album_2.){}
        albums[albums.length] = album_2;
    }
    //JSON.stringify(albums);
    //将js对象转化成json字符串。
    //alert(albums.length);
    $.cookie('albums', JSON.stringify(albums), {path:"/"});
    set_itemVal(albums);
   
}

//album类的JS版本构造函数
function album() {
    this.title = $('#biao_value').text();
    this.img = $('#img').attr('src');
    this.id = $('#id').val();
    this.count = $('#amount_value').val();
    this.price = $('#price_value').text();
}
//解析COokie中的Json。
function parse_json_cart() {
    var album_1 = $.cookie('albums');
    
    var object = JSON.parse(album_1);
    //alert(object.length);
    set_shopping_cart(object);
    set_itemVal(object);
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
//得到商品对象数组。
function get_item_array() {
    var album_1 = $.cookie('albums');
    //alert(album_1);
    var object = JSON.parse(album_1);
    return object;
    
}
//删除数组中的商品。
function delete_item(id) {
    var albums = get_item_array();
    var albums =albums.splice(id, 1);
    $.cookie('albums', JSON.stringify(albums), { path: "/" });
    set_itemVal(albums);
}
//设置购物车商品数目
function set_itemVal(ob1) {
    $('#p_counts').text(ob1.length);
}
function set_shopping_cart(ob1) {
    for (var i in ob1) {

        var str = ".item_info_" + i;
        $(str).find(' #cart_img').attr('src', ob1[i].img);
        $(str).find(' #cart_title').text(ob1[i].title);
        $(str).find('#item_price').text(ob1[i].price);
        $(str).find('#cart_count').text(ob1[i].count);
        
    }
    $('#counts').text(ob1.length);
    $('#total').text('$' + sum(ob1));
    $('#p_length').val(ob1.length);
}