<template>
    <section>
        <el-row class="details-row">
            <el-collapse v-model="activeNames">
                <el-collapse-item title="合同基本信息" name="1">
                    <el-col :span="8">
                        <label>客户编号：</label>
                        <span>00000121</span>
                    </el-col>
                    <el-col :span="8">
                        <label>合同编号：</label>
                        <span>CXM-BJYZ-RZ-20170801</span>
                    </el-col>
                    <el-col :span="8">
                        <label>签署类型：</label>
                        <span>续签</span>
                    </el-col>
                    <el-col :span="8">
                        <label>合同模板：</label>
                        <span>养老机构服务合同20170301</span>
                    </el-col>
                    <el-col :span="8">
                        <label>合同开始日期：</label>
                        <span>2017-08-01</span>
                    </el-col>
                    <el-col :span="8">
                        <label>合同截至时间：</label>
                        <span>2018-07-31</span>
                    </el-col>
                    <el-col :span="8">
                        <label>签约销售：</label>
                        <span>苗苗</span>
                    </el-col>
                    <el-col :span="8">
                        <label>签约日期：</label>
                        <span>2017-08-01</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="甲方信息（养老服务机构）" name="2">
                    <el-col :span="8">
                        <label>项目全称：</label>
                        <span>远洋·椿萱茂（北京亦庄）老年公寓</span>
                    </el-col>
                    <el-col :span="8">
                        <label>甲方：</label>
                        <span>北京椿萱茂凯健养老服务有限公司</span>
                    </el-col>
                    <el-col :span="8">
                        <label>法定代表人：</label>
                        <span>罗剑</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系电话：</label>
                        <span>010-85146166</span>
                    </el-col>
                    <el-col :span="8">
                        <label>银行户名：</label>
                        <span>北京椿萱茂凯健养老服务有限公司</span>
                    </el-col>
                    <el-col :span="8">
                        <label>开户行：</label>
                        <span>中国银行股份有限公司北京劲松东口支行</span>
                    </el-col>
                    <el-col :span="8">
                        <label>银行账号：</label>
                        <span>3259 6052 0796</span>
                    </el-col>
                    <el-col :span="8">
                        <label>电子邮箱：</label>
                        <span>电子邮箱</span>
                    </el-col>
                    <el-col :span="8">
                        <label>邮政编码：</label>
                        <span>710000</span>
                    </el-col>
                    <el-col :span="8">
                        <label>地址：</label>
                        <span>北京市大兴区亦庄镇经海二路2号及2号院1号楼</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="乙方（入住老年人）" name="3">
                    <el-col :span="8">
                        <label>乙方姓名：</label>
                        <span>薛玉英</span>
                    </el-col>
                    <el-col :span="8">
                        <label>性别：</label>
                        <span>女</span>
                    </el-col>
                    <el-col :span="8">
                        <label>年龄：</label>
                        <span>75</span>
                    </el-col>
                    <el-col :span="8">
                        <label>居民身份证号：</label>
                        <span>0111111111111111</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系电话：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>电子邮箱：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>地址：</label>
                        <span>北京市朝阳区</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="乙方监护人" name="4">
                    <el-col :span="8">
                        <label>乙方监护人姓名：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>性别：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>与乙方关系：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>居民身份证号：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>手机号码：</label>
                        <span>13999999999</span>
                    </el-col>
                    <el-col :span="8">
                        <label>固定电话：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>工作单位：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>电子邮箱：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>通讯地址：</label>
                        <span>北京市朝阳区</span>
                    </el-col>
                    <el-col :span="8">
                        <label>邮政编码：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>经常居住地地址：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>乙方监护人（非自然人）名称：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>法人代表：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>电子邮箱：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系人：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>手机号码：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>固定电话：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>公司地址：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>邮政编码：</label>
                        <span></span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="丙方" name="5">
                    <el-col :span="8">
                        <label>丙方作为入住老人的：</label>
                        <span>付款义务人,连带责任保证人,联系人,代理人</span>
                    </el-col>
                    <el-col :span="8">
                        <label>丙方姓名：</label>
                        <span>张健</span>
                    </el-col>
                    <el-col :span="8">
                        <label>性别：</label>
                        <span>女</span>
                    </el-col>
                    <el-col :span="8">
                        <label>与乙方关系：</label>
                        <span>直系亲属</span>
                    </el-col>
                    <el-col :span="8">
                        <label>居民身份证号：</label>
                        <span>110105196406296527</span>
                    </el-col>
                    <el-col :span="8">
                        <label>手机号码：</label>
                        <span>13999999999</span>
                    </el-col>
                    <el-col :span="8">
                        <label>固定电话：</label>
                        <span>01080897606</span>
                    </el-col>
                    <el-col :span="8">
                        <label>工作单位：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>电子邮箱：</label>
                        <span>Tomhe6225@sina.com</span>
                    </el-col>
                    <el-col :span="8">
                        <label>通讯地址：</label>
                        <span>北京市朝阳区</span>
                    </el-col>
                    <el-col :span="8">
                        <label>邮政编码：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>经常居住地地址：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>丙方（非自然人）名称：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>法人代表：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>电子邮箱：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系人：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>手机号码：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>固定电话：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>公司地址：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>邮政编码：</label>
                        <span></span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="房间信息" name="6">
                    <el-col :span="8">
                        <label>服务地点：</label>
                        <span>北京市大兴区亦庄镇经海二路2号及2号院1号楼</span>
                    </el-col>
                    <el-col :span="8">
                        <label>是否包房：</label>
                        <span>包房</span>
                    </el-col>
                    <el-col :span="8">
                        <label>楼栋：</label>
                        <span>1栋</span>
                    </el-col>
                    <el-col :span="8">
                        <label>所属单元：</label>
                        <span>1单元</span>
                    </el-col>
                    <el-col :span="8">
                        <label>所属楼层：</label>
                        <span>5层</span>
                    </el-col>
                    <el-col :span="8">
                        <label>房间号：</label>
                        <span>510</span>
                    </el-col>
                    <el-col :span="8">
                        <label>床位号：</label>
                        <span>1</span>
                    </el-col>
                    <el-col :span="8">
                        <label>房型：</label>
                        <span>标准间-A1</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="服务项目" name="7">
                    <el-col :span="8">
                        <label>生活能力类型：</label>
                        <span>生活照料3级</span>
                    </el-col>
                    <el-col :span="8">
                        <label>服务包级别：</label>
                        <span>3</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="收费信息" name="8">
                    <el-col :span="8">
                        <label>押金类型：</label>
                        <span>第一人押金</span>
                    </el-col>
                    <el-col :span="8">
                        <label>押金：</label>
                        <span>80000.00</span>
                    </el-col>
                    <el-col :span="8">
                        <label>一次性安置费：</label>
                        <span>0.00</span>
                    </el-col>
                    <el-col :span="8">
                        <label>收费类型：</label>
                        <span>标准收费</span>
                    </el-col>
                    <el-col :span="8">
                        <label>床位费：</label>
                        <span>6700.00</span>
                    </el-col>
                    <el-col :span="8">
                        <label>餐费：</label>
                        <span>2400.00</span>
                    </el-col>
                    <el-col :span="8">
                        <label>基础服务费：</label>
                        <span>500.00</span>
                    </el-col>
                    <el-col :span="8">
                        <label>服务包费用：</label>
                        <span>5500.00</span>
                    </el-col>
                    <el-col :span="8">
                        <label>失智服务费：</label>
                        <span>0.00</span>
                    </el-col>
                    <el-col :span="8">
                        <label>附加服务费：</label>
                        <span>0.00</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="请假退费信息" name="9">
                    <el-col :span="8">
                        <label>餐费：</label>
                        <span>60.00</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="违约金信息" name="10">
                    <el-col :span="8">
                        <label>违约金：</label>
                        <span>9060.00</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="老带新信息" name="11">
                    <el-col :span="8">
                        <label>推荐人：</label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <label>推荐人奖励积分：</label>
                        <span>0</span>
                    </el-col>
                    <el-col :span="8">
                        <label>被推荐人奖励积分：</label>
                        <span>0</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="审批信息" name="12">
                    <el-col :span="8">
                        <label>下载：</label>
                        <span>CXM-BJYZ-RZ-20170801薛玉英.PDF</span>
                    </el-col>
                </el-collapse-item>
            </el-collapse>
        </el-row>
        <el-row class="button-bottom-row">
            <el-button type="primary">立即创建</el-button>
            <el-button>取消</el-button>
        </el-row>
    </section>
</template>
<script>
import {
    provinceAndCityData,
    regionData,
    provinceAndCityDataPlus,
    regionDataPlus,
    CodeToText,
    TextToCode
} from "element-china-area-data";

export default {
    data() {
        return {
            disabledType: false,
            activeNames: ["1", "2", "3", "4", "5"],
            dialogForm: {},
            dialogFormVisible: false,
            form: {
                name: "",
                region: "为长辈咨询",
                date1: "",
                date2: "",
                delivery: false,
                type: [],
                resource: "",
                desc: "",
                age: 0
            },
            moreSearch: false,
            currentPage: 15,
            filters: {},
            tableData: [
                {
                    date: "2016-05-02",
                    name: "王小虎",
                    address: "上海市普陀区金沙江路 1518 弄"
                },
                {
                    date: "2016-05-04",
                    name: "王小虎",
                    address: "上海市普陀区金沙江路 1517 弄"
                },
                {
                    date: "2016-05-01",
                    name: "王小虎",
                    address: "上海市普陀区金沙江路 1519 弄"
                },
                {
                    date: "2016-05-03",
                    name: "王小虎",
                    address: "上海市普陀区金沙江路 1516 弄"
                }
            ],
            options: regionData,
            selectedOptions: []
        };
    },

    methods: {
        typeChange(val) {
            // this.disabledType = val === "1" ? true : false;
            this.disabledType = true;
        }
    }
};
</script>
<style>
</style>
