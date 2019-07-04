class Validators {
    validateAmount(rule, value, callback) {
        if (parseFloat(value).toString() === "NaN") {
            callback(new Error("请输入正确金额"));
        }
        if (value <= 0) {
            callback(new Error("金额必须大于0"));
        }
        callback();
    }

    validateNumberRange(rule, value, callback) {
        if (parseFloat(value).toString() === "NaN") {
            callback(new Error("请输入正确数字"));
        }
        if (value <= rule.min) {
            callback(new Error(rule.fieldName + "必须大于" + rule.min));
        }
        if (value >= rule.max) {
            callback(new Error(rule.fieldName + "必须小于" + rule.max));
        }
        callback();
    }

    validateNumberFullRange(rule, value, callback) {
        if (parseFloat(value).toString() === "NaN") {
            callback(new Error("请输入正确数字"));
        }
        if (value <= rule.min) {
            callback(new Error(rule.fieldName + "必须大于" + rule.min));
        }
        if (value > rule.max) {
            callback(new Error(rule.fieldName + "必须小于" + rule.max));
        }
        callback();
    }

    validateAmountEqualZero(rule, value, callback) {
        if (parseFloat(value).toString() === "NaN") {
            callback(new Error("请输入正确金额"));
        }
        if (value < 0) {
            callback(new Error("金额必须大于等于0"));
        }
        callback();
    }

    validateAmountIsNull(rule, value, callback) {
        if (value) {
            if (parseFloat(value).toString() === "NaN") {
                callback(new Error("请输入正确金额"));
            }
            if (value <= 0) {
                callback(new Error("金额必须大于0"));
            }
        }
        callback();
    }

    validateHomeServicePirce(rule, value, callback) {
        if (parseFloat(value).toString() === "NaN") {
            callback(new Error("请输入正确金额"));
        }
        if (value < 0) {
            callback(new Error("金额必须大于等于0"));
        }
        callback();
    }

    validateInteger(rule, value, callback) {
        const intValue = parseInt(value, 10);
        if (intValue.toString() === "NaN" || intValue <= 0) {
            let msg = "请输入正整数";
            if (rule.message) {
                msg = rule.message;
            }
            callback(new Error(msg));
        }
        callback();
    }

    zipCode(rule, value, callback) {
        var re = /^[1-9][0-9]{5}$/;
        if (!re.test(value) && value) {
            let msg = "邮政编码格式错误";
            if (rule.message) {
                msg = rule.message;
            }
            callback(new Error(msg));
        }
        callback();
    }

    phone(rule, value, callback) {
        var re = /^1[3|4|5|7|8][0-9]\d{8}$/;
        if (!re.test(value) && value) {
            let msg = "手机号码格式错误";
            if (rule.message) {
                msg = rule.message;
            }
            callback(new Error(msg));
        }
        callback();
    }

    idNumber(rule, value, callback) {
        if (!value) {
            callback();
            return;
        }
        let msg = undefined;

        if (typeof value !== "string") msg = "非法字符串";
        var city = {
            11: "北京",
            12: "天津",
            13: "河北",
            14: "山西",
            15: "内蒙古",
            21: "辽宁",
            22: "吉林",
            23: "黑龙江 ",
            31: "上海",
            32: "江苏",
            33: "浙江",
            34: "安徽",
            35: "福建",
            36: "江西",
            37: "山东",
            41: "河南",
            42: "湖北 ",
            43: "湖南",
            44: "广东",
            45: "广西",
            46: "海南",
            50: "重庆",
            51: "四川",
            52: "贵州",
            53: "云南",
            54: "西藏 ",
            61: "陕西",
            62: "甘肃",
            63: "青海",
            64: "宁夏",
            65: "新疆",
            71: "台湾",
            81: "香港",
            82: "澳门",
            91: "国外"
        };
        var birthday = value.substr(6, 4) + "/" + Number(value.substr(10, 2)) + "/" + Number(value.substr(12, 2));
        var d = new Date(birthday);
        var newBirthday = d.getFullYear() + "/" + Number(d.getMonth() + 1) + "/" + Number(d.getDate());
        var currentTime = new Date().getTime();
        var time = d.getTime();
        var arrInt = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
        var arrCh = ["1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2"];
        var sum = 0,
            i,
            residue;

        if (!/^\d{17}(\d|x)$/i.test(value)) msg = "非法身份证";
        if (city[value.substr(0, 2)] === undefined) msg = "非法地区";
        if (time >= currentTime || birthday !== newBirthday) msg = "非法生日";
        for (i = 0; i < 17; i++) {
            sum += value.substr(i, 1) * arrInt[i];
        }
        residue = arrCh[sum % 11];
        if (residue !== value.substr(17, 1)) msg = "非法身份证哦";

        msg = city[value.substr(0, 2)] + "," + birthday + "," + (value.substr(16, 1) % 2 ? " 男" : "女");
        if (msg) {
            if (rule.message) {
                msg = rule.message;
            }
            callback(new Error(msg));
        }
        callback();
    }

    bankAccount(rule, value, callback) {
        var re = /^[0-9]{8,20}$/;
        if (!re.test(value)) {
            let msg = "格式错误";
            if (rule.message) {
                msg = rule.message;
            }
            callback(new Error(msg));
        }
        callback();
    }

    stringTrim(str) {
        return str.replace(/(^\s*)|(\s*$)/g, "");
    }

    validateArray(rule, value, callback) {
        if (value && value.length > 0) {
            callback();
        } else {
            callback(new Error(rule.message));
        }
    }
    phone(rule, value, callback) {
        if (value && value.length > 0) {
            callback();
        } else {
            callback(new Error(rule.message));
        }
    }
    idcard(rule, value, callback) {
        if (!value) {
            callback();
            return;
        }
        let msg = undefined;

        if (typeof value !== "string") msg = "非法字符串";
        var city = {
            11: "北京",
            12: "天津",
            13: "河北",
            14: "山西",
            15: "内蒙古",
            21: "辽宁",
            22: "吉林",
            23: "黑龙江 ",
            31: "上海",
            32: "江苏",
            33: "浙江",
            34: "安徽",
            35: "福建",
            36: "江西",
            37: "山东",
            41: "河南",
            42: "湖北 ",
            43: "湖南",
            44: "广东",
            45: "广西",
            46: "海南",
            50: "重庆",
            51: "四川",
            52: "贵州",
            53: "云南",
            54: "西藏 ",
            61: "陕西",
            62: "甘肃",
            63: "青海",
            64: "宁夏",
            65: "新疆",
            71: "台湾",
            81: "香港",
            82: "澳门",
            91: "国外"
        };
        var birthday = value.substr(6, 4) + "/" + Number(value.substr(10, 2)) + "/" + Number(value.substr(12, 2));
        var d = new Date(birthday);
        var newBirthday = d.getFullYear() + "/" + Number(d.getMonth() + 1) + "/" + Number(d.getDate());
        var currentTime = new Date().getTime();
        var time = d.getTime();
        var arrInt = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
        var arrCh = ["1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2"];
        var sum = 0,
            i,
            residue;

        if (!/^\d{17}(\d|x)$/i.test(value)) msg = "非法身份证";
        if (city[value.substr(0, 2)] === undefined) msg = "非法地区";
        if (time >= currentTime || birthday !== newBirthday) msg = "非法生日";
        for (i = 0; i < 17; i++) {
            sum += value.substr(i, 1) * arrInt[i];
        }
        residue = arrCh[sum % 11];
        if (residue !== value.substr(17, 1)) msg = "非法身份证哦";

        msg = city[value.substr(0, 2)] + "," + birthday + "," + (value.substr(16, 1) % 2 ? " 男" : "女");
        if (msg) {
            if (rule.message) {
                msg = rule.message;
            }
            callback(new Error(msg));
        }
        callback();
    }
}

const v = new Validators();
const ValidationMethods = {
    items: [
        { value: "idcard", label: "身份证号", pattern: /(^[1-9]\d{5}(18|19|([23]\d))\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$)|(^[1-9]\d{7}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}$)/ },
        { value: "mobile", label: "手机号码", pattern: /^1[3|4|5|7|8][0-9]{9}$/ },
        { value: "url", label: "网址", pattern: /^(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-.,@?^=%&:/~+#]*[\w\-@?^=%&/~+#])?$/ },
    ],
    // [Description("文本")]
    //     Text,

    //     [Description("数字")]
    //     Number,

    //     [Description("多行文本")]
    //     Textarea,

    //     [Description("日期")]
    //     Date,

    //     [Description("图片")]
    //     Image,

    //     [Description("下拉选框")]
    //     Dropdown,

    //     [Description("多选框")]
    //     Checkbox,

    //     [Description("单选框")]
    //     Radio,

    //     [Description("开关")]
    //     Switch,

    //     [Description("富文本")]
    //     RichText
    inputs: {
        text: { label: "文本", trigger: "blur" },
        number: { label: "数字", trigger: "blur" },
        textarea: { label: "多行文本", trigger: "blur" },
        date: { label: "日期", trigger: "change" },
        image: { label: "图片", trigger: "change" },
        dropdown: { label: "下拉选框", trigger: "change" },
        checkbox: { label: "多选框", trigger: "change" },
        radio: { label: "单选框", trigger: "change" },
        switch: { label: "开关", trigger: "change" },
        rich_text: { label: "富文本", trigger: "blur" },
    },
    getInputs() {
        return Object.keys(this.inputs).map(x => ({ value: x, label: this.inputs[x].label }))
    },
    isFormatValidation(method) {
        return this.items.findIndex(x => x.value === method) > -1
    },
    generateRules(customForm) {
        return customForm.fieldGroups
            .map(x => x.fields)
            .reduce((x, y) => x.concat(y))
            .filter(field => field.input && field.input.validationMethods && field.input.validationMethods.length)
            .reduce((rules, field) => {
                rules[field.name] = this.generateRule(field)
                return rules
            }, {})
    },
    generateRule(field) {
        let validationMethods = [
            {
                method: 'required',
                validator: (field, method) => ({
                    required: true,
                    message: `请输入${field.label}`,
                    trigger: this.inputs[field.input.type].trigger
                })
            },
        ]

        validationMethods = validationMethods.concat(this.items.map(x => {
            return {
                validator: (field, method) => ({
                    pattern: this.items.filter(x => x.value === method.method)[0].pattern,
                    message: `无效的${field.label}`,
                    trigger: this.inputs[field.input.type].trigger
                }),
                method: x.value
            }
        }))

        return field.input.validationMethods.map(inputValidationMethod =>
            validationMethods.filter(x => x.method === inputValidationMethod.method)[0].validator(field, inputValidationMethod)
        )
    }
};

// accountNumber: [
//     {
//         required: true,
//         message: "请输入账户号码",
//         trigger: "blur"
//     },
//     { validator: validators.bankAccount, trigger: "blur" }
// ],

export { ValidationMethods }
export default v;


// {
//     required: true,
//     message: "请输入机构名称",
//     trigger: "blur"
// }


