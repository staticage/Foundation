 <template>
  <section class="apartmentIndex">
    <el-row class="button-top-row">
      <el-button type="primary" size="mini" @click="create" v-if="$hasPermission('新增')" icon="el-icon-circle-plus">新增</el-button>
      <el-button type="primary" size="mini" @click="create" disabled icon="el-icon-success">启用</el-button>
      <el-button type="primary" size="mini" @click="create" disabled icon="el-icon-remove-outline">失效</el-button>
    </el-row>
    <el-row class="search-row el-form el-form--inline">
      <el-col :span="24">
        <label v-if="user.supperAdmin">所属公司：</label>
        <el-select v-if="user.supperAdmin" v-model="query.companyId" placeholder="请选择所属公司">
          <el-option v-for="item in companies" :key="item.id" :label="item.name" :value="item.id"></el-option>
        </el-select>
        <label>公寓名称：</label>
        <el-input v-model="query.name" placeholder="公寓名称"></el-input>
        <label>公寓类型：</label>
        <div class="filter-content" style="height:auto">
          <SingleSelector :data="companyType" v-model="query.type"  @selected="newSearch"></SingleSelector>
        </div>
        <!--<el-select v-model="query.type" placeholder="请选择公寓类型">-->
          <!--<el-option label="全部" value></el-option>-->
          <!--<el-option label="CC" :value="1"></el-option>-->
          <!--<el-option label="CB" :value="2"></el-option>-->
          <!--<el-option label="CLRC" :value="3"></el-option>-->
        <!--</el-select>-->
        <el-button type="primary" icon="el-icon-search" @click="search()">查询</el-button>
      </el-col>
    </el-row>
    <el-row :gutter="20" style="margin-top: 20px;">
      <el-col
        :xs="24"
        :sm="12"
        :md="10"
        :lg="7"
        :xl="5"
        v-for="(item,index) in resource.items"
        :key="index"
      >
        <div class="border_box">
          <div class="img_box">
            <!--<img src="./../../../assets/images/home.png" alt="">-->
            <img src="./../../../assets/images/home.png" :alt="item.name">
            <span class="titleName">{{item.name}}</span>
          </div>
          <div class="desc_box">
            <div class="desc">
              <p>所属公司</p>
              <p>{{item.companyName}}</p>
            </div>
            <div class="desc1">
              <p>公寓类型</p>
              <p>{{item.typeText}}</p>
            </div>
          </div>
          <div class="icon_box">
            <span class="span_">
              <el-tooltip
                      effect="dark"
                      content="查看"
                      placement="bottom"
              >
                <i class="el-icon-view" @click="details(item)"></i>
              </el-tooltip>

            </span>
            <span class="span_">
              <el-tooltip
                      effect="dark"
                      content="编辑"
                      placement="bottom"
              >
                <i class="el-icon-edit" @click="edit(item)"></i>
              </el-tooltip>

            </span>
            <span class="span_">
              <el-tooltip
                      effect="dark"
                      content="删除"
                      placement="bottom"
              >
                <i class="el-icon-delete" @click="del(item.id)"></i>
              </el-tooltip>

            </span>
            <el-dropdown trigger="click" style="margin-left: 42px;cursor: pointer">
              <span class="el-dropdown-link">
                <el-tooltip
                        effect="dark"
                        content="更多"
                        placement="bottom"
                >
                <i class="el-icon-more"></i>
              </el-tooltip>
              </span>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item>
                  <div @click="goRoom(item)">房间信息</div>
                </el-dropdown-item>
                <!-- <el-dropdown-item>哈哈哈</el-dropdown-item>
                <el-dropdown-item>哈哈哈</el-dropdown-item>-->
              </el-dropdown-menu>
            </el-dropdown>
          </div>
        </div>
      </el-col>
    </el-row>

    <!--<el-row class="table-row">-->
    <!--<el-table :data="resource.items">-->
    <!--<el-table-column type="selection" width="55"></el-table-column>-->
    <!--<el-table-column prop="companyName" label="所属公司" v-if="user.supperAdmin"></el-table-column>-->
    <!--<el-table-column prop="serialNumber" label="公寓编号"></el-table-column>-->
    <!--<el-table-column prop="name" label="公寓名称"></el-table-column>-->
    <!--<el-table-column prop="typeText" label="公寓类型"></el-table-column>-->
    <!--<el-table-column prop="area" label="区域"></el-table-column>-->
    <!--<el-table-column prop="address" label="地址"></el-table-column>-->
    <!--<el-table-column fixed="right" label="操作">-->
    <!--<template slot-scope="scope">-->
    <!--<el-button @click="room(scope.row)" type="text">房间信息</el-button>-->
    <!--<el-button @click="details(scope.row)" type="text">查看</el-button>-->
    <!--<el-button @click="edit(scope.row)" type="text">编辑</el-button>-->
    <!--<el-button type="text" @click="del(scope.row.id)">删除</el-button>-->
    <!--</template>-->
    <!--</el-table-column>-->
    <!--</el-table>-->
    <!--<el-pagination-->
    <!--:current-page="query.page"-->
    <!--:page-sizes="[10, 20, 30, 40]"-->
    <!--:page-size="query.pageSize"-->
    <!--layout="total,prev,pager,next,sizes,jumper"-->
    <!--:total="resource.total"-->
    <!--@size-change="handleSizeChange"-->
    <!--@current-change="handleCurrentChange"-->
    <!--&gt;</el-pagination>-->
    <!--</el-row>-->
  </section>
</template>
<script>
import SearchButton from "../../../components/SearchButton.vue";
import api from "../../../config/api";
import axios from "../../../utils/http";
import SingleSelector from "../../../components/SingleSelector.vue";

export default {
  components: { SearchButton, SingleSelector },
  data() {
    return {
        company: {
            type: null
        },
        companyType: [
            {
                value: null,
                name: "全部"
            },
            {
                value: 1,
                name: "CC"
            },
            {
                value: 2,
                name: "CB"
            },
            {
                value: 3,
                name: "CLRC"
            }],
      query: {
          type:null
      },
      loading: false,
      resource: {},
      companies: [],
      user: {},
      userList: [
        {
          imgUrl: require("./../../../assets/images/home.png"),
          name: "诚和敬",
          hyuser: "活跃用户",
          adduser: "新增用户"
        },
        {
          imgUrl: require("./../../../assets/images/home.png"),
          name: "诚和敬",
          hyuser: "活跃用户",
          adduser: "新增用户"
        },
        {
          imgUrl: require("./../../../assets/images/home.png"),
          name: "诚和敬",
          hyuser: "活跃用户",
          adduser: "新增用户"
        },
        {
          imgUrl: require("./../../../assets/images/home.png"),
          name: "诚和敬",
          hyuser: "活跃用户",
          adduser: "新增用户"
        },
        {
          imgUrl: require("./../../../assets/images/home.png"),
          name: "诚和敬",
          hyuser: "活跃用户",
          adduser: "新增用户"
        }
      ]
    };
  },

  async mounted() {
    this.user = this.$store.state.user;
    this.companies = (await axios.get("company/select-list")).data;
    this.search();
  },
  methods: {
      newSearch() {
          this.query.page = 1;
          this.search();
      },
    handleSizeChange(pageSize) {
      this.query.pageSize = pageSize;
      this.search();
    },

    handleCurrentChange(page) {
      this.query.page = page;
      this.search();
    },
    async search() {
      this.resource = (await axios.post("apartment/query", this.query)).data;
    },
    create() {
      this.$router.push({
        // name: "apartment-create"
        path: "/system/apartment/create"
      });
    },

    edit(row) {
      this.$router.push({
        // name: "apartment-create",
        path: "/system/apartment/edit/" + row.id + "/false"
        // params: { id: row.id }
      });
    },
    details(row) {
      this.$router.push({
        path: "/system/apartment/details/" + row.id + "/true"
        // name: "apartment-create",
        // params: {
        //     id: row.id,
        //     disabled: "true"
        // }
      });
    },

    goRoom(row) {
      this.$router.push({
        path: "/system/apartment/room/" + row.id
        // name: "apartment-room/"
      });
    },

    del(id) {
      this.$confirm("确认删除?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          axios.delete("apartment/" + id).then(
            response => {
              this.loading = false;
              this.$message({
                type: "success",
                message: "删除成功!"
              });
              this.search();
            },
            response => {
              this.loading = false;
              const result = response.response.data;
              this.$message({
                type: "error",
                message: "删除失败:" + result.error,
                center: true
              });
            }
          );
          // try {
          //     axios.delete("apartment/" + id);
          //     this.loading = false;
          //     this.$message({
          //         type: "success",
          //         message: "删除成功!"
          //     });
          //     this.search();
          // } catch (e) {
          //     console.debug(e);
          // }
        })
        .catch(() => {
          // catch 不要删除，Uncaught (in promise) cancel
        });
    }
  }
};
</script>
 <style lang="scss">

     .el-tooltip__popper.is-dark {
       background: #42b983;
       color: #fff;
     }
     .el-tooltip__popper[x-placement^="bottom"] .popper__arrow::after,.el-tooltip__popper[x-placement^="bottom"] .popper__arrow {
       border-bottom-color: #42b983;
       border-bottom-color: #42b983;
     }

 </style>
 <style lang="scss" scoped>
p {
  margin: 0;
}
.border_box {
  border: 1px solid #ccc;
  border-radius: 3px;
  margin-bottom: 15px;
  box-shadow: 0.02rem 0.03rem 0.3rem #cccccc;
  .img_box {
    width: 100%;
    position: relative;
    height: 60px;
    padding-left: 15px;
    box-sizing: border-box;
    padding-top: 10px;
    img {
      display: inline-block;
      width: 50px;
      height: 50px;
    }
    .titleName {
      font-size: 16px;
      display: inline-block;
      position: absolute;
      top: 24px;
      left: 80px;
    }
  }
  .desc_box {
    width: 100%;
    height: 100px;
    color: #7e7e7e;
    .desc {
      float: left;
      margin-top: 15px;
      width: 230px;
      p {
        padding: 0px 70px 5px 80px;
        font-size: 14px;
      }
      p:nth-of-type(2) {
        font-size: 16px;
        white-space: nowrap;
        text-overflow: ellipsis;
        overflow: hidden;
      }
    }
    .desc1 {
      width: 85px;
      float: left;
      margin-top: 15px;
      p {
        padding-bottom: 5px;
        font-size: 14px;
      }
      p:nth-of-type(2) {
        font-size: 16px;
        white-space: nowrap;
        text-overflow: ellipsis;
        overflow: hidden;
      }
    }
  }
  .icon_box {
    width: 100%;
    height: 40px;
    background: #fafafa;
    line-height: 40px;
    color: #7e7e7e;
    .span_ {
      width: 25%;
      border-right: 1px solid #ccc;
      display: inline-block;
      float: left;
      text-align: center;
      cursor: pointer;
      height: 25px;
      line-height: 25px;
      margin-top: 8px;
    }
  }
}
</style>
