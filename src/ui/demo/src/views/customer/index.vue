<style lang="scss">
    .customInput {
        @media screen and (max-width: 1366px) {
            .search-row .el-select,
            .search-row .el-input {
                width: 150px;
            }
        }
    }
</style>
<template>
    <section class="customInput">
        <el-row class="search-row el-form el-form--inline">
            <el-col :gutter="20">
                    <label>公寓名称：</label>
                    <el-select v-model="query.apartmentId" placeholder="请选择公寓">
                        <el-option label="全部" value></el-option>
                        <el-option
                                v-for="item in apartments"
                                :key="item.id"
                                :label="item.name"
                                :value="item.id"
                        ></el-option>
                    </el-select>

                    <label>客户姓名：</label>
                    <el-input placeholder="客户姓名" v-model="query.name"></el-input>

                    <label>房&nbsp;间&nbsp;号&nbsp;：</label>
                    <el-input placeholder="房间号" v-model="query.roomName"></el-input>


                    <label>客户编号：</label>
                    <el-input placeholder="客户编号" v-model="query.customerNo"></el-input>
                    <el-button type="primary" icon="el-icon-search" @click="search">查询</el-button>
                    <el-button type="info" plain @click="query={}" icon="el-icon-refresh">重置</el-button>
                <!-- <el-col :span="4">
            <label>是否包房：</label>
            <el-input placeholder="备选条件2"></el-input>
                </el-col>-->
            </el-col>
        </el-row>
        <!--<SearchButton v-model="isMore" style="margin-bottom:18px;"></SearchButton>-->
        <!-- Waiting to Check in  -->
        <el-tabs v-model="activeName" @tab-click="handleClick" type="border-card" class="table-row">
            <!-- <el-tab-pane label="待入住客户" name="third">
                <el-table :data="tableCheckInData">
                    <el-table-column prop="a" label="入住公寓"></el-table-column>
                    <el-table-column prop="b" label="客户编号"></el-table-column>
                    <el-table-column prop="c" label="客户姓名"></el-table-column>
                    <el-table-column prop="d" label="客户性别"></el-table-column>
                    <el-table-column prop="e" label="客户年龄"></el-table-column>
                    <el-table-column prop="f" label="入住房间"></el-table-column>
                    <el-table-column prop="g" label="是否包房"></el-table-column>
                    <el-table-column fixed="right" label="操作">
                        <template slot-scope="scope">
                            <el-button @click="details(scope.row,'waiting')" type="text">查看</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <el-pagination
                    :pager-count="4"
                    :current-page="currentPage"
                    :page-sizes="[10, 20, 30, 40]"
                    :page-size="100"
                    layout="total,prev,pager,next,sizes,jumper"
                    :total="400"
                ></el-pagination>
            </el-tab-pane>-->
            <el-tab-pane label="入住客户" name="1">
                <el-table :data="resource.items">
                    <el-table-column prop="currnetApartmentName" label="入住公寓"></el-table-column>
                    <el-table-column prop="customerNo" label="客户编号"></el-table-column>
                    <el-table-column prop="name" label="客户姓名"></el-table-column>
                    <el-table-column prop="sexText" label="客户性别"></el-table-column>
                    <el-table-column prop="age" label="客户年龄"></el-table-column>
                    <el-table-column prop="bedName" label="入住房间"></el-table-column>
                    <el-table-column prop="isCompartment" label="是否包房">
                        <template slot-scope="scope">
                            <el-tag v-if="scope.row.isCompartment!= undefined"
                                    :type="scope.row.isCompartment == true ? 'success' : 'danger'"
                                    disable-transitions
                            >{{scope.row.isCompartment == true ? '包房' : '拼房'}}
                            </el-tag>
                        </template>
                    </el-table-column>
                    <el-table-column fixed="right" label="操作">
                        <template slot-scope="scope">
                            <el-button @click="details(scope.row,'details')" type="text">查看</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <el-pagination
                        :current-page="query.page"
                        :page-sizes="[10, 20, 30, 40]"
                        :page-size="query.pageSize"
                        layout="total,prev,pager,next,sizes,jumper"
                        :total="resource.total"
                        @size-change="handleSizeChange"
                        @current-change="handleCurrentChange"
                ></el-pagination>
            </el-tab-pane>
            <el-tab-pane label="退住客户" name="2">
                <el-table :data="tableCheckInData">
                    <el-table-column prop="a" label="入住公寓"></el-table-column>
                    <el-table-column prop="customerNo" label="客户编号"></el-table-column>
                    <el-table-column prop="c" label="客户姓名"></el-table-column>
                    <el-table-column prop="sex" label="客户性别"></el-table-column>
                    <el-table-column prop="e" label="客户年龄"></el-table-column>
                    <el-table-column prop="f" label="入住房间"></el-table-column>
                    <el-table-column prop="g" label="是否包房"></el-table-column>
                    <el-table-column fixed="right" label="操作">
                        <template slot-scope="scope">
                            <el-button @click="details(scope.row,'waiting')" type="text">查看</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <!--<el-pagination-->
                <!--:pager-count="4"-->
                <!--:current-page="currentPage"-->
                <!--:page-sizes="[10, 20, 30, 40]"-->
                <!--:page-size="100"-->
                <!--layout="total,prev,pager,next,sizes,jumper"-->
                <!--:total="400"-->
                <!--&gt;</el-pagination>-->
            </el-tab-pane>
            <el-tab-pane label="全部" name="3">
                <el-table :data="tableCheckInData">
                    <el-table-column prop="b" label="客户编号"></el-table-column>
                    <el-table-column prop="c" label="客户姓名"></el-table-column>
                    <el-table-column prop="d" label="客户性别"></el-table-column>
                    <el-table-column prop="e" label="客户年龄"></el-table-column>
                    <el-table-column fixed="right" label="操作">
                        <template slot-scope="scope">
                            <el-button @click="details(scope)" type="text">查看</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <!--<el-pagination-->
                <!--:pager-count="4"-->
                <!--:current-page="currentPage"-->
                <!--:page-sizes="[10, 20, 30, 40]"-->
                <!--:page-size="100"-->
                <!--layout="total,prev,pager,next,sizes,jumper"-->
                <!--:total="400"-->
                <!--&gt;</el-pagination>-->
            </el-tab-pane>
        </el-tabs>
    </section>
</template>
<script>
    import SearchButton from "../../components/SearchButton.vue";
    import api from "../../config/api";

    export default {
        data() {
            return {
                query: {},
                resource: {},
                apartments: [],
                // isMore: false,
                activeName: "1",
                tableCheckInData: [
                    {
                        a: "公寓1",
                        b: "00000080",
                        c: "步新杰",
                        d: "男",
                        e: "97",
                        f: "512",
                        g: "包房"
                    },
                    {
                        a: "公寓2",
                        b: "00000121",
                        c: "孙国凡",
                        d: "女",
                        e: "75",
                        f: "510",
                        g: "单床"
                    },
                    {
                        a: "公寓1",
                        b: "00000080",
                        c: "步新杰",
                        d: "男",
                        e: "97",
                        f: "512",
                        g: "包房"
                    },
                    {
                        a: "公寓2",
                        b: "00000121",
                        c: "孙国凡",
                        d: "女",
                        e: "75",
                        f: "510",
                        g: "单床"
                    },
                    {
                        a: "公寓1",
                        b: "00000080",
                        c: "步新杰",
                        d: "男",
                        e: "97",
                        f: "512",
                        g: "包房"
                    },
                    {
                        a: "公寓2",
                        b: "00000121",
                        c: "孙国凡",
                        d: "女",
                        e: "75",
                        f: "510",
                        g: "单床"
                    }
                ]
            };
        },

        async mounted() {
            this.apartments = (await this.$axios.get("apartment/select-list")).data;
            this.search();
        },

        methods: {
            handleSizeChange(pageSize) {
                this.query.pageSize = pageSize;
                this.search();
            },

            handleCurrentChange(page) {
                this.query.page = page;
                this.search();
            },
            async search() {
                this.resource = (await this.$axios.post(
                    api.customer + "/query",
                    this.query
                )).data;
            },
            details(row, type) {
                let path;
                switch (type) {
                    case "waiting":
                        path = "/customer/waiting/" + row.id;
                        break;
                    case "details":
                        path = "/customer/details/" + row.id;
                        break;
                    default:
                        break;
                }
                this.$router.push({path});
            },
            handleClick() {
            }
        },
        components: {
            SearchButton
        }
    };
</script>