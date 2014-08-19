//页面初始化
function init() {
    $('button,button,#button').button();
   
    $(document).tooltip();

}
//对数量增加或减少button的click事件逻辑判断
function amount(val) {
    var value = val;
    
    if (value == '+') {
        
        $('#amount_value').val(parseInt($('#amount_value').val())+1); 
    } else if (value == '-' && $('#amount_value').val() > 1) {
        $('#amount_value').val(parseInt($('#amount_value').val()) - 1);
    }
}
//判断用户商品数量是否为数字，如果不是数字将强行修改为数字。
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
//添加到购物车相应事件
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
                //将要添加的商品个数。
                var item_count = parseInt(album_2.count);
                var albums_count=parseInt(albums[i].count)
                albums[i].count = item_count + albums_count;
                break;
                //如果i==数组的长度-1，表示数组遍历完也没有找到同类商品。
            } else if(i==albums.length-1) {
                albums[albums.length] = album_2;
            }
        }
        //albums[albums.length] = album_2;
    } else {
        
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
    if (album_1 != null) {
        var object = JSON.parse(album_1);
    }
    else {
        return;
    }
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
    var total_str = total.toString();
    if ((total_str.length-total_str.indexOf(".")) > 4) {
        total_str = total.toString().substring(0, total_str.indexOf(".")+2);
    }else{
        total_str=total.toString();
    }
    
    return total_str;
}
//从cookie中得到商品对象数组。
function get_item_array() {
    var album_1 = $.cookie('albums');
    if (album_1 != null) {
        var object = JSON.parse(album_1);
        return object;
    }
    else {
        return;
    }
    
    
}
//删除数组中的商品。
function delete_item(id) {
    var albums = get_item_array();
    
    //删除数组中的元素，并且返回要删除的元素。
     albums.splice(id, 1);
   
    $.cookie('albums', JSON.stringify(albums), { path: "/" });
    parse_json_cart();
}
//设置购物车商品数目
function set_itemVal(ob1) {
    $('#p_counts').text(ob1.length);
}

//替换公共页面中的购物车的数据，
function set_shopping_cart(ob1) {
    $('.fenge').children().remove();
    //根据数组长度生成相应的列表。
    for(var i=0;i<ob1.length;i++){
        $('.fenge').append($('#cart_album').clone());
           }
    var cart_album = $(".fenge").children("#cart_album");
    for (var i in ob1) {
        cart_album.eq(i).removeClass("item_info(0)");
        //动态生成class类名。
        var str_1 = "item_info_" + i;
        cart_album.eq(i).addClass(str_1);
        cart_album.show();
        var str = ".item_info_" + i;
         //为符合条件下得各个元素赋值。    
        $(str).find('#cart_img').attr('src', ob1[i].img);
        //替换专辑中的值。
        $(str).find('#cart_title a').text(ob1[i].title);
        //替换专辑中的价格。
        $(str).find('#item_price').text(ob1[i].price);
        //替换商品的数量。
        $(str).find('#cart_count').text(ob1[i].count);
        $(str).find('#p_id').val(ob1[i].id);
        //替换删除商品时的点击事件。
        $(str).find('#delete').attr("onclick", 'delete_item(' + i + ')');
        $(str).find('#p-img a').attr('href', "/store/details?id=" + ob1[i].id);
        $(str).find('#cart_title a').attr('href', "/store/details?id=" + ob1[i].id);
    }
    
    $('.counts').text(ob1.length);
    $('#total').text('$' + sum(ob1));
    
}
//设置搜索按钮的click点击事件。
function search() {
    var search_val = $('.search').val();
   
        window.location.href ="/home/Search?key=" + search_val;
      
}
//设置分页链接的href属性。
function setPageHref() {

    $('.page_text').attr('href',"home/search?page=1");
}
//根据用户输入关键字对查询到的专辑标题进行动态添加样式。
function setKeywordClass() {
    var names=$('#p_name a');
    var title = names.text();
    var keyword = $('#key').val().toString();
    for (var i = 0; i < names.size() ; i++) {

        $('#zhuanji_brwose').children("li").eq(i).addClass(i.toString());
        title = names.eq(i).text();
        if (title.match(keyword) == keyword) {
            title = title.replace(keyword , keyword.fontcolor("red"));
            names.eq(i).html(title);
        }
    }
   
}
//设置购物车页面的主要数据
function setCart_table() {
    
    var ob1 = get_item_array();
        for (var i in ob1) {
            var tr = $('#cart_table').find('tr');
       //根据需求动态向table添加行
            tr.eq(i).after('<tr><td><input class="cartitem_number" type="hidden" value="' + i + '"/>'
                + ob1[i].title +
         '</td><td>' + ob1[i].price + '</td><td>'
         +'<ul id="number" >'
            +'<li id="amount">'
               
                +'<input id="amount_sub" class="amount_button" type="button" value="-" />'
               
                +'<input id="amount_value" name="amount" value="'+ob1[i].count+'" />'
                +'<input id="amount_add" class="amount_button" type="button" value="+" />'
            +'</li>' + '</td><td><input id="delete_cart_item"type=button value="删除"></td></tr>');
      
        }
        $('#cartTotal_price').text('$' + sum(ob1));
    
}
//主要针对购物车主页面中的数量进行操作。
function cart_amount() {
    var button = $(this);
    //alert();
    mutiAmount(button);
}
//针对购物车页面中的商品选择，进行逻辑操作。
function mutiAmount(ob1) {
    var button = ob1;
    var rownum = getrownum(button);
   
    var amount = getamount_val(rownum);
    //对得到的值转成int
    var amount_num=parseInt(amount.val());
    if (button.val() == '+') {
        amount.val(amount_num + 1);
    } else if (button.val() == '-' && amount.val() > 1) {
        amount.val(amount_num - 1);
    }
    
}
//得到button点击的行号
function getrownum(ob1) {
    var button = ob1;
    var number = button.parents().find('.cartitem_number').val();
    var rownum = parseInt(number);
    return rownum;
}
//得到button点击时，得到数量输入框对象。
function getamount_val(rownum) {
    var tr = $('#cart_table').find('tr');
    //找到按钮点击的数量输入框。
    var amount_val = tr.eq(rownum + 1).find('#amount_value');
    
    return amount_val;
}
