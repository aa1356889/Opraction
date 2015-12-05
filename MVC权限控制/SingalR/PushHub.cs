using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebHelper;

namespace SingalR
{
   public class PushHub:Hub
    {

       protected static IHubContext ZmHubContext = GlobalHost.ConnectionManager.GetHubContext<PushHub>();
       /// <summary>
       /// 保存当前服务器在线人数信息
       /// </summary>
       static IList<User> users = new List<User>();
       /// <summary>
       /// 重写连接事件
       /// </summary>
       /// <returns></returns>
       public override System.Threading.Tasks.Task OnConnected()
       {
           //判断连接是否存在  如果不存在则加入在线集合
           if (!users.Any(c => c.ConnctionId == Context.ConnectionId))
           {
               users.Add(new User() { ConnctionId = Context.ConnectionId, LoginName =HttpContext.Current.User.Identity.Name  });
           }
           return base.OnConnected();
       }


       /// 断开连接的事件
       /// </summary>
       /// <returns></returns>
       public override Task OnDisconnected(bool stopCalled)
       {
           var user = users.Where(u => u.ConnctionId == Context.ConnectionId).FirstOrDefault();

           //判断用户是否存在,存在则删除
           if (user != null)
           {
               //删除用户
               users.Remove(user);

           }
           return base.OnDisconnected(stopCalled);
       }


       /// <summary>
       /// 给指定用户发送信息
       /// </summary>
       /// <param name="loginName"></param>
       /// <param name="message"></param>
       public void Send(string loginName, string message,Func<Message> okExcete)
       {
           var user = users.SingleOrDefault(c => c.LoginName == loginName);
           //表示用户离线状态
           if (user == null)
           {
               Message center = new Message() { Content = message, RevidUser = loginName, SendUser = HttpContext.Current.User.Identity.Name };
               //仍回队列
               Redis.RedisManager.Lpush(keyS.Message, center);
           }
           else
           {
               //成功发送执行回调函数  比如物理化消息数据
               if (okExcete != null)
               {
                   okExcete();
               }
               //像指定连接客户端发送请求
               ZmHubContext.Clients.Client(user.ConnctionId).notice(message);
           }
       }

    }
}
