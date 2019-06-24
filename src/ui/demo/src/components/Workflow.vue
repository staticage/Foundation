<template>
    <div>
        <el-row>
            <el-col>
                <el-steps direction="vertical" :active="-1">
                    <el-step :title="`${item.name}：${item.roleName}`" :description="item.time"
                             v-for="(item,index) in steps.history" :key="index">
                        <div slot="description">
                            <div>
                                <p>{{item.time}}</p>
                                <p>{{item.accept?'同意':'不同意'}}</p>
                                <p>{{item.description}}</p>
                            </div>
                        </div>
                    </el-step>
                </el-steps>
                <el-steps direction="vertical" :active="active">
                    <el-step :title="`${item.name}：${item.roleName}`" :description="item.time"
                             v-for="(item,index) in steps.runningWorkflowSteps" :key="index">
                        <div slot="description">
                            <div v-if="active - 1 > index">
                                <p>{{item.time}}</p>
                                <p>{{item.accept?'同意':'不同意'}}</p>
                                <p>{{item.description}}</p>
                            </div>
                            <div v-if="active - 1 == index">
                                <div style="margin-top:10px">
                                    <el-radio v-model="item.accept" :label="true" :disabled="status!=0">同意</el-radio>
                                    <el-radio v-model="item.accept" :label="false" :disabled="status!=0">不同意</el-radio>
                                </div>
                                <el-input :disabled="status!=0" type="textarea" :rows="5"
                                          style="margin-top:10px;height: 100px; resize:none;" placeholder="请输入同意/不同意理由"
                                          resize="none" v-model="item.description"></el-input>
                            </div>
                        </div>
                    </el-step>
                </el-steps>
            </el-col>
        </el-row>
        <el-row>
            <el-col>
                <el-button v-if="status==0" type="primary"
                           style="float:left;margin-left: 20px;" @click="handleSubmit">提交
                </el-button>
                <!-- <el-button type="warning">废弃</el-button>
                <el-button type="warning">生效</el-button>
                 <el-button type="primary">打印</el-button>
                <el-button type="primary">上传</el-button>-->
            </el-col>
        </el-row>
    </div>
</template>
<script>
    import axios from "axios";

    export default {
        props: {
            workFlowId: String,
            businessId: String,
            backlogId: String,
            type:String
        },
        data() {
            return {
                steps: [],
                disabled: true,
                active: 2,
                status: 0,
                handler:{
                    "contract": this.submitContract,
                    "roomChange":this.submitRoomChange,
                    "servicePackChange":this.submitServicePackChange
                }
            };
        },
        async mounted() {
            if (this.workFlowId) {
                const backlog = (await axios.get(
                    "user/backlog/status/" + this.backlogId
                )).data;
                this.status = backlog.status;

                const steps = (await axios.get(
                    "workflow/details/" + this.businessId
                )).data;
                let userId = JSON.parse(localStorage.getItem("user")).id;
                steps.runningWorkflowSteps.forEach((step, index) => {
                    if (step.active) {
                        this.active = index + 1;
                    }
                });
                this.steps = steps;
            }
        },
        methods: {
            handleSubmit() {
              this.handler[this.type]();
            },
            submitContract() {
                let activeStep = this.steps.runningWorkflowSteps[this.active - 1];
                let data = {
                    accept: activeStep.accept,
                    description: activeStep.description,
                    runningWorkFlowId: this.workFlowId,
                    contractId: this.businessId
                };
                this.submit("contract/submit",data);
            },
            submitRoomChange() {
                let activeStep = this.steps.runningWorkflowSteps[this.active - 1];
                let data = {
                    accept: activeStep.accept,
                    description: activeStep.description,
                    runningWorkFlowId: this.workFlowId,
                    roomChangeId: this.businessId
                };
                this.submit("contract/room-change",data);
            },
            submitServicePackChange() {
                let activeStep = this.steps.runningWorkflowSteps[this.active - 1];
                let data = {
                    accept: activeStep.accept,
                    description: activeStep.description,
                    runningWorkFlowId: this.workFlowId,
                    liveTypeChangeId: this.businessId
                };
                this.submit("contract/service-pack-change",data);
            },
            submit(url,data) {
                axios.post(url, data)
                    .then(response => {
                        this.saving = false;
                        this.$message({
                            message: "提交成功",
                            type: "success"
                        });
                        this.$router.go(0);
                    })
                    .catch(err => {
                        this.saving = false;
                        this.$message({
                            message: "提交失败," + err.message,
                            type: "error"
                        });
                    });
            }
        }
    };
</script>

<style scoped>
    .el-switch {
        height: 70px;
        /* float: left; */
    }

    .el-steps.el-steps--vertical > div {
        height: 180px;
    }

    .el-steps--vertical {
        padding: 15px;
    }
</style>
