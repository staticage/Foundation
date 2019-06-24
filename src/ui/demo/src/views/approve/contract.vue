<template>
    <div>
        <el-row :gutter="16">
            <el-col :span="16">
                <Contract v-if="contractId&&status!=2" :contractId="contractId"></Contract>
                <EditContract v-if="contractId&&status==2" :contractId="contractId"></EditContract>
            </el-col>
            <el-col :span="8">
                <Workflow v-if="workFlowId&&contractId&&backlogId" :businessId="contractId"
                          :workFlowId="workFlowId" :backlogId="backlogId" :status="status" type="contract"></Workflow>
            </el-col>
        </el-row>
    </div>
</template>

<script>
    import Workflow from "../../components/Workflow.vue";
    import Contract from "../contract/details.vue";
    import EditContract from "../contract/edit.vue";
    import qs from "querystring";

    export default {
        components: {
            Workflow,
            Contract,
            EditContract
        },
        data() {
            return {
                workFlowId: "",
                contractId: "",
                backlogId:"",
                status: -1
            };
        },
        mounted() {
            if (this.$route.query.rid) {
                this.workFlowId = this.$route.query.rid;
                this.$axios.get("workFlow/status/" + this.workFlowId).then(response => {
                    this.status = response.data;
                }).catch(err => {
                    console.log(err);
                })
            }
            if (this.$route.query.cid) {
                this.contractId = this.$route.query.cid;
            }
            if (this.$route.query.bid) {
                this.backlogId = this.$route.query.bid;
            }
        }
    };
</script>

