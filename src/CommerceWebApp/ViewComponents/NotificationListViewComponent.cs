using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Commerce_WebApp.Data;
using Commerce_WebApp.Models;

namespace Commerce_WebApp.ViewComponents
{
    public class NotificationListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public NotificationListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }
        
        private Task<List<Notification>> GetItemsAsync()
        {
            return _context.Notification.FromSqlInterpolated($"ReturnUnreadNotifications {User.Identity.Name}").ToListAsync();
        }
    }
}
