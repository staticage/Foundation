<template>
    <section class="changeRoom">
        <el-row>
            <el-col :span="24">
                <div class="contentTitle">
                    <i class="el-icon-refresh"></i>
                    房型变更
                </div>
            </el-col>
        </el-row>
            <el-row :gutter="20" style="width: 70%;float: left;padding: 20px 100px;">
                <el-card class="box-card"  style="margin-top: 25px;padding: 20px;">
                <el-col :span="24">
                    <el-form ref="form" :model="roomChange">
                        <el-col :span="24" class="pb-20">
                            <span>餐费：</span>{{roomChange.mealsCost}}
                        </el-col>
                        <el-col :span="24" class="pb-20">
                            <span>房型：</span>{{roomChange.roomType}}
                        </el-col>
                        <el-col :span="24" class="pb-20">
                            <span>床位费：</span>{{roomChange.roomCost}}
                        </el-col>
                        <el-col :span="24" class="pb-20">
                            <span>床位：</span>{{roomChange.bedName}}
                        </el-col>
                        <el-col :span="24" class="pb-20">
                            <span class="letter_">是否包房：</span>
                                <el-radio-group v-model="roomChange.isCompartment" :disabled="true">
                                    <el-radio :label="false">拼房</el-radio>
                                    <el-radio :label="true">包房</el-radio>
                                </el-radio-group>
                        </el-col>
                        <el-col :span="24" class="pb-20">
                            <span>基础服务费：</span>{{roomChange.serviceCost}}
                        </el-col>
                        <el-col :span="24" class="pb-20">
                            <span class="letter_"> 结束日期：</span>
                                <el-date-picker v-model="roomChange.endDate" :disabled="true" type="date" placeholder="选择日期"></el-date-picker>
                        </el-col>
                        <el-col :span="24" class="pb-20">
                            <span class="letter_"> 变更日期：</span>
                                <el-date-picker v-model="roomChange.changeDate" :disabled="true" type="date" placeholder="选择日期"></el-date-picker>
                        </el-col>
                    </el-form>
                </el-col>
                </el-card>
            </el-row>
            <el-row style="width: 30%;float: right">
                <el-col :span="24" style="padding: 20px 100px 0 0px;">
                    <Workflow v-if="workFlowId&&roomChangeId&&backlogId" :businessId="roomChangeId"
                              :workFlowId="workFlowId" :backlogId="backlogId" :status="status" type="roomChange"></Workflow>
                </el-col>
            </el-row>
    </section>
</template>

<script>
    import Workflow from "../../components/Workflow.vue";
    import api from "../../config/api";

    export default {
        components: {
            Workflow
        },
        data() {
            return {
                workFlowId: "",
                roomChangeId: "",
                backlogId:"",
                status: -1,
                roomChange: {},
            };
        },
        mounted() {
            const {rid, wid, bid} = this.$router.currentRoute.query;
            if (rid) {
                this.roomChangeId = rid;
                this.$axios.get(`${api.roomChangeDetails}/${this.roomChangeId}`).then(response => {
                    this.roomChange = response.data;
                }).catch(err => {
                    console.log(err);
                })
            }
            if (wid) {
                this.workFlowId = wid;
                this.$axios.get(`${api.workflowStatus}/${this.workFlowId}`).then(response => {
                    this.status = response.data;
                }).catch(err => {
                    console.log(err);
                });
            }
            if(bid) {
                this.backlogId = bid;
            }
        }
    };
</script>
<style lang="scss">
    .changeRoom {
        .box-card .el-card__body {
            color: #666;
            font-weight:normal;
        }
    }
</style>
<style lang="scss" scoped>
.changeRoom {
    .contentTitle{
        background: #f5f7fa;
        padding: 5px;
        box-sizing:border-box;
        color: #606266;
        font-size: 14px;
    }
    .letter {
        letter-spacing: 23px;
    }
    .letter_ {
        letter-spacing: 4px;
    }
    .letter__ {
        letter-spacing:10px;
    }
    .pb-20 {
        padding-bottom: 20px;
        color:#666;
    }
}

</style>

