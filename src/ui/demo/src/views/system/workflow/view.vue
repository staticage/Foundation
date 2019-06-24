<template>
    <section>
        <el-row class="top-label">
            <el-row :gutter="20" class="descriptTitle">
                <el-col :span="6">
                    <label>工作流名称：{{workflow.name}}</label>
                </el-col>
                <el-col :span="6">
                    <label>工作流类型：{{workflow.type}}</label>
                </el-col>
            </el-row>
        </el-row>
        <el-row style="margin: 200px auto 0;box-sizing: border-box">
            <el-col :span="22">
                <el-steps style="height: 400px">
                    <el-step
                        :title="item.name"
                        v-for="item in workflow.steps"
                        :description="item.roleName"
                        :key="item.id"
                    ></el-step>
                </el-steps>
            </el-col>
        </el-row>

        <el-row class="button-bottom-row operation_btn">
            <el-button @click="goBack">返回</el-button>
        </el-row>
    </section>
</template>
<script>
import api from "../../../config/api";

export default {
    data() {
        return {
            workflow: {}
        };
    },

    mounted() {
        const { id } = this.$route.params;
        this.$axios
            .get(api.role + "/select-list")
            .then(response => {
                this.roles = response.data;
                this.$axios
                    .get(api.workflow + "/" + id)
                    .then(res => {
                        let data = res.data;

                        data.steps = data.steps.map(x => {
                            let roleId = x.command.roleId;
                            let roleName = _.find(
                                this.roles,
                                x => x.id == roleId
                            ).name;
                            return { ...x, roleId: roleId, roleName: roleName };
                        });
                        this.workflow = data;
                        console.log(res.data, "123");
                    })
                    .catch(err => {});
            })
            .catch(err => {});
    },

    methods: {
        goBack() {
            this.$router.go(-1);
        }
    }
};
</script>
<style scoped>
.descriptTitle {
    background: #f5f7fa;
    padding: 10px;
    color: #666;
    border-radius: 5px;
    box-sizing: border-box;
    font-size: 14px;
}
.chooseType {
    text-align: right;
    vertical-align: middle;
    float: left;
    font-size: 12px;
    color: #606266;
    line-height: 33px;
    padding: 0 12px 0 0;
    -webkit-box-sizing: border-box;
    box-sizing: border-box;
    height: 50px;
}

.stepBox {
    float: left;
    width: 60%;
    color: #333;
    margin-top: 20px;
}

.el-step__title {
    font-size: 14px;
}

.chooseTypeTitle {
    margin-left: 10px;
}

.nameType {
    width: 100%;
}
</style>
<style>
.el-step__title.is-wait,
.el-step__description.is-wait {
    color: #333;
}

.el-input--small .el-input__inner {
    width: 200px;
}

.el-step__icon {
    background: #73dacf;
}

.el-step__icon.is-text {
    border: 1px solid #73dacf;
}

.el-step__icon-inner {
    color: #fff;
}

.top-label {
    margin-top: 10px;
    margin-bottom: 10px;
}
</style>
