let serAddress = document.querySelector("#adressStatment").innerHTML;
let json = JSON.parse(serAddress);
let strAdress = '';

startAddress(8);
const dataList = document.getElementById('adressList');
dataList.innerHTML = strAdress;

input.oninput = function () {
    if (input.value == "") {
        startAddress(8);
    }
    else if {

    }
};

function startAddress(i) {
    for (let key in json) {
        if (json[key].Level == i) {
            strAdress += `
            <option>${json[key].ShortTypeName} ${json[key].AddressName}</option><br/>
                `;
        }
    }
}