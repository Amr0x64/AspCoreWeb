let allAddress = JSON.parse(document.querySelector("#adressStatment").innerHTML);
//let streetNumList = JSON.parse(document.querySelector("#streetNumber").innerHTML);

let checkFirst = true;
let inputAddress = '';
let addressList = [];
let resultAddressInput = '';
let fulladdress = '';

function event() {
    console.log("ЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫ");
    /*select2 = $('.select2');*/
    $('.select2').on('select2:closing', function () {
        $('.select2').select2('open');
    });

    $(document).ready(function () { // Возвращает обьект выбора
        $('.select2').on('select2:select', function (e) {
            inputAddress.value = e.params.data.text;
        });
    });
}

let tempParent = {};

$(".selectpill").select2({
    data: [{id: 1, text: "1"}],
    closeOnSelect: false,
    tokenSeparators: [',', ' '],
    placeholder: "Ы"
}).select2("open");

inputAddress = document.getElementsByClassName('select2-search__field');
inputAddress = inputAddress[0];
inputAddress.oninput = search;

function search() {
    console.log("Onimput");
    if (inputAddress.value == "") {          //empty input
        console.log("Пустой инпкт");
        addressList = [];
        resultAddressInput = "";
        checkFirst = true;

        $(".selectpill").select2({
            data: [],
            closeOnSelect: false,
            tokenSeparators: [',', ' ']
        }).trigger('change');

        //inputAddress = document.getElementsByClassName('select2-search__field');
        //inputAddress = inputAddress[0];
        //inputAddress.oninput = search;
    }
    else {
        resultAddressInput = inputAddress.value.split(" ");
        resultAddressInput = resultAddressInput[resultAddressInput.length - 1];
        if (checkFirst) {
            for (let address in allAddress) {
                if ((allAddress[address].level == 5 || allAddress[address].level == 8 || allAddress[address].level == 7) &&
                    (allAddress[address].address_name.toLowerCase().indexOf(resultAddressInput.toLowerCase()) == 0)) {

                    tempParent = allAddress.find(p => p.fias_guid == allAddress[address].parent_id);
                    addressList.push({
                        id: 1, level: allAddress[address].level, text: `${tempParent.address_name}, ${allAddress[address].short_type_name} -
                                ${allAddress[address].address_name} `
                    });
                }
            }
            if (addressList.length != 0) {  
                addressList.sort((prev, next) => next.level - prev.level);
                $('.selectpill').select2('destroy');

                $(".selectpill").select2({
                    data: addressList,
                    closeOnSelect: false,
                    tokenSeparators: [',', ' ']
                }).select2("open").trigger('change');
                checkFirst = false;
    
                inputAddress = document.getElementsByClassName('select2-search__field');
                inputAddress = inputAddress[0];
                inputAddress.oninput = search;
            }
        }
        else if (resultAddressInput == "") {   //поиск доч-их элементов
            /*searchChild();*/
            console.log("Мортра аутист");
        }
    }
}

function searchChild() {
    /*$('.select2').val(null).trigger('change');*/
}