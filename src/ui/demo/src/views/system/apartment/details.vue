<template>
    <section>
        <el-row class="details-row">
            <el-collapse v-model="activeNames">
                <el-collapse-item title="公寓信息" name="1">
                    <el-col :span="8">
                        <label>公寓编号：</label>
                        <span>0001250</span>
                    </el-col>
                    <el-col :span="8">
                        <label>公寓名称：</label>
                        <span>公寓名称</span>
                    </el-col>
                    <el-col :span="8">
                        <label>公寓地点：</label>
                        <span>公寓地点</span>
                    </el-col>
                    <el-col :span="8">
                        <label>所属城市：</label>
                        <span>陕西省西安市</span>
                    </el-col>
                    <el-col :span="8">
                        <label>公寓状态：</label>
                        <span>有效</span>
                    </el-col>
                    <el-col :span="8">
                        <label>公寓全称：</label>
                        <span>公寓全称</span>
                    </el-col>
                    <el-col :span="8">
                        <label>公寓类型：</label>
                        <span>CLRC</span>
                    </el-col>
                    <el-col :span="8">
                        <label>管理区域：</label>
                        <span>省区</span>
                    </el-col>
                    <el-col :span="8">
                        <label>楼栋数：</label>
                        <span>1</span>
                    </el-col>
                    <el-col :span="8">
                        <label>单元数：</label>
                        <span>3</span>
                    </el-col>
                    <el-col :span="8">
                        <label>楼层数：</label>
                        <span>21</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="公司信息" name="2">
                    <el-col :span="8">
                        <label>公司名称：</label>
                        <span>公司名称</span>
                    </el-col>
                    <el-col :span="8">
                        <label>公司法人：</label>
                        <span>公司法人</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系电话：</label>
                        <span>联系电话</span>
                    </el-col>
                    <el-col :span="8">
                        <label>账户名称：</label>
                        <span>账户名称</span>
                    </el-col>
                    <el-col :span="8">
                        <label>账户号码：</label>
                        <span>账户号码</span>
                    </el-col>
                    <el-col :span="8">
                        <label>开户行：</label>
                        <span>开户行</span>
                    </el-col>
                    <el-col :span="8">
                        <label>邮政编码：</label>
                        <span>邮政编码</span>
                    </el-col>
                    <el-col :span="8">
                        <label>电子邮箱：</label>
                        <span>电子邮箱</span>
                    </el-col>
                    <el-col :span="8">
                        <label>公司地址：</label>
                        <span>公司地址</span>
                    </el-col>
                    <el-col :span="8">
                        <label>服务地点：</label>
                        <span>服务地点</span>
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
    components: { AreaSelector },
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
