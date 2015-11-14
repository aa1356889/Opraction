using Jurisdiction.Entity;
using Jurisdiction.Entity.Extend;
using Jurisdiction.EntityView;
using Jurisdiction.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHelper;

namespace Jurisdiction.UI.Areas.Admin.WebBase
{
    /// <summary>
    /// 视图父类
    /// </summary>
    public abstract class BasePage :WebViewPage
    {
        IBLL.OprationIBLL _opracitonbll = null;
        public override void InitHelpers()
        {
            _opracitonbll = Jurisdiction.UI.App_Start.AutofacRegister.Resove<IBLL.OprationIBLL>();
            base.InitHelpers();
            BaseHelper = new BaseHelper(Url);
        }

        /// <summary>
        /// 当前用户的一些信息
        /// </summary>
        public MyFormsPrincipal<UserTick> UserInfo { get; set; }

        /// <summary>
        /// 帮助类
        /// </summary>
        public BaseHelper BaseHelper { get; set; }


        /// <summary>
        /// 当前用户所属权限
        /// </summary>
        public List<OpractionsExtend> Opracton
        {

            get
            {
                //先从缓存中拿
                List<OpractionsExtend> opractions = CaheManager.GetData<List<OpractionsExtend>>(UserInfo._userData.LoginName);
                if (opractions == null)
                {
                    //如果没有查询数据库
                    opractions = _opracitonbll.GetOpractionByUid(UserInfo._userData.UId);
                    //存入缓存
                    CaheManager.SetData(UserInfo._userData.UId.ToString(), opractions, 30);
                }
                return opractions;
            }
        }

    }

    public abstract class BasePage<T> : WebViewPage<T> where T : class
    {
        IBLL.OprationIBLL _opracitonbll = null;
        public override void InitHelpers()
        {
            base.InitHelpers();
            _opracitonbll = Jurisdiction.UI.App_Start.AutofacRegister.Resove<IBLL.OprationIBLL>();
            BaseHelper = new BaseHelper(Url);

        }
        /// <summary>
        /// 页面辅助基类
        /// </summary>
        public BaseHelper BaseHelper
        {
            get;
            set;
        }

        /// <summary>
        /// 当前用户所属权限
        /// </summary>
        public List<OpractionsExtend> Opracton
        {

            get
            {
                //先从缓存中拿
                List<OpractionsExtend> opractions = CaheManager.GetData<List<OpractionsExtend>>(UserInfo._userData.LoginName);
                if (opractions == null)
                {
                    //如果没有查询数据库
                    opractions = _opracitonbll.GetOpractionByUid(UserInfo._userData.UId);
                    //存入缓存
                    CaheManager.SetData(UserInfo._userData.UId.ToString(), opractions, 30);
                }
                return opractions;
            }
        }

        public MyFormsPrincipal<UserTick> UserInfo
        {
            get
            {
                return HttpContext.Current.User as MyFormsPrincipal<UserTick>;
            }
        }
    }
}