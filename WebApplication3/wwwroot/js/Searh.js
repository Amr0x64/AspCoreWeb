let serAddress = document.querySelector("#adressStatment").innerHTML;
let streeatNumbers = document.querySelector("#streetNumber").innerHTML;

let json = JSON.parse(serAddress);
let streetNumList = JSON.parse(streeatNumbers);

let strAdress = '';
const dataList = document.getElementById('adressList');
let statmentList = [];
let tempStatmentList = [];
let fullAddress = "";
let checkApart = false;
startAddress(8);

input.oninput = function () {
    if (input.value == "") {
        fullAddress = "";
        checkApart = false;
        startAddress(8);
    }
    else if ((input.value[input.value.length - 1]) == " ") {
        let result = "";
        for (let i = input.value.length - 1; i >= 0; i--) {
            if ((input.value[i] == "-") && (input.value[i + 1]) == " ") {
                break; 
            }
            else { 
                result += input.value[i];
            }
        }
        result = result.slice(1, result.length - 1);
        result = result.split('').reverse().join('');
        //Изменить
        if (checkApart) {
            for (let key in statmentList) {
                if (statmentList[key].HouseNumber == result) {
                    fullAddress += `д - ${statmentList[key].HouseNumber}, `;
                    for (let child in streetNumList) {
                        if (streetNumList[child].FiasGuid == statmentList[key].FiasGuid) {
                            strAdress += `
                                <option value="${fullAddress} кв - ${streetNumList[child].FlatNumber}">${streetNumList[child].FlatNumber}</option><br/>
                            `;
                        }
                    }
                    if (strAdress != "") {
                        dataList.innerHTML = "";
                        dataList.innerHTML = strAdress;
                        strAdress = "";
                    }
                    checkApart = false;
                    break;
                }
            }
        }
        else {
            for (let key in statmentList) {
                if (statmentList[key].address_name == result) {
                    fullAddress += `${statmentList[key].short_type_name} - ${statmentList[key].address_name}, `;
                    tempStatmentList = statmentList.slice(0);
                    statmentList = [];
                    for (let child in json) {
                        if (json[child].parent_id == tempStatmentList[key].fias_guid) {
                            statmentList.push(json[child]);
                            strAdress += `
                            <option value="${fullAddress} ${json[child].short_type_name} - ${json[child].address_name}">${json[child].address_name}</option><br/>
                        `;
                        }
                    }
                    if (strAdress == "") {
                        checkApart = true;
                        for (let apart in streetNumList) {
                            if (tempStatmentList[key].fias_guid == streetNumList[apart].FiasGuid) {
                                statmentList.push(streetNumList[apart]);
                                strAdress += `
                                <option value="${fullAddress} д - ${streetNumList[apart].HouseNumber}">${streetNumList[apart].HouseNumber}</option><br/>
                            `;
                            }
                        }
                    }
                    if (strAdress != "") {
                        dataList.innerHTML = "";
                        dataList.innerHTML = strAdress;
                        strAdress = "";
                    }   
                    break;
                }
            }
        }
    }
};

function startAddress(i) {
    for (let key in json) {
        if (json[key].level == i) {
            statmentList.push(json[key]);
            strAdress += `
            <option>${json[key].short_type_name} - ${json[key].address_name}</option><br/>
                `;
        }
    }
    dataList.innerHTML = strAdress;
    strAdress = "";
}