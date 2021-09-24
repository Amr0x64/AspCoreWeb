let serAddress = document.querySelector("#adressStatment").innerHTML;
let streeatNumbers = document.querySelector("#streetNumber").innerHTML;

let allAddress = JSON.parse(serAddress);
let streetNumList = JSON.parse(streeatNumbers);

let inputAddress = '';
let addressList = [];
//spis.push({ id: '1', text: "Kunka" }, { id: 1, text: "Mortra" }, { id: 1, text: "Pudge" } );

$(document).ready(function () { // Инициализация
    $('.select2').select2({
        placeholder: "Введите адрес",
        data: addressList,
        closeOnSelect: false,
    }).select2('open');
});

$(document).ready(function () { // Запрет закрытия
    $('.select2').on('select2:closing', function () {
        $('.select2').select2('open')
    });
});

$(document).ready(function () {
    $('.select2-search__field').focus(function () {
        inputAddress = document.getElementsByClassName('select2-search__field');
        inputAddress = inputAddress[0];
        inputAddress.oninput = search;
    });
});

function search() {
    
}

