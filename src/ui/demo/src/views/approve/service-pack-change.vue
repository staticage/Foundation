<template>
    <section class="changeService">
        <el-row>
            <el-col :span="24">
                <div class="contentTitle">
                    <i class="el-icon-refresh"></i>
                    服务包变更
                </div>
            </el-col>
        </el-row>
        <el-row :gutter="20" style="width: 70%;float: left;padding: 20px 100px;">
            <el-card class="box-card"  style="margin-top: 25px;padding: 20px;">
                 <el-col :span="24">
                <el-form ref="form" :model="servicePackChange">
                    <el-col :span="24" class="pb-20">
                        <span class="letter_">变更日期：</span>
                        <el-date-picker :disabled="true" v-model="servicePackChange.changeDate" type="date" placeholder="选择日期"></el-date-picker>
                    </el-col>
                    <el-col :span="24" class="pb-20">
                        <span>生活能力类型：</span>{{servicePackChange.servicePackType}}
                    </el-col>
                    <el-col :span="24" class="pb-20">
                        <span>生活能力费用：</span>{{servicePackChange.servicePackPrice}}
                    </el-col>
                    <el-col :span="24" class="pb-20">
                        <span>附加服务费：</span>{{servicePackChange.attachedServiceCost}}
                    </el-col>
                </el-form>
            </el-col>
            </el-card>
            <el-col :span="8">

            </el-col>
        </el-row>
        <el-row style="width: 30%;float: right">
            <el-col :span="24" style="padding: 20px 100px 0 0px;">
                <Workflow v-if="workFlowId&&servicePackChangeId&&backlogId" :businessId="servicePackChangeId"
                          :workFlowId="workFlowId" :backlogId="backlogId" :status="status" type="servicePackChange">
                </Workflow>
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
                servicePackChangeId: "",
                backlogId:"",
                status: -1,
                servicePackChange: {},
            };
        },
        mounted() {
            const {sid, wid, bid} = this.$router.currentRoute.query;
            if (sid) {
                this.servicePackChangeId = sid;
                this.$axios.get(`${api.servicePackChangeDetails}/${this.servicePackChangeId}`).then(response => {
                    this.servicePackChange = response.data;
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
    .changeService {
        .box-card .el-card__body {
            color: #666;
            font-weight:normal;
        }
    }
</style>
<style lang="scss" scoped>
    .changeService {
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
            letter-spacing: 7px;
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



