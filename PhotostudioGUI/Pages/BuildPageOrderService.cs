using PhotostudioDLL.Entities.Services;

namespace PhotostudioGUI.Pages;

/// <summary>
///     Сборка элементов для информации про услугу
/// </summary>
internal static class BuildPageOrderService
{
    /// <summary>
    ///     Заполнение данными услуги
    /// </summary>
    internal static List<TextBlock> BuildPage(OrderService service)
    {
        var listBlock = new List<TextBlock>();
        var margin = new Thickness(5);
        var title = new TextBlock
        {
            FontSize = 15, FontWeight = FontWeights.Medium, Text = $"Название услуги: {service.Service.Title}",
            Margin = margin
        };
        var employeeName = new TextBlock
        {
            FontSize = 15, Text = $"Сотрудник: {service.Employee.FullName}",
            Margin = margin
        };
        var status = new TextBlock
        {
            FontSize = 15, Text = $"Статус: {service.TextStatus}",
            Margin = margin
        };
        listBlock.Add(title);
        listBlock.Add(employeeName);
        listBlock.Add(status);
        switch (service.Service.Type)
        {
            case Service.ServiceType.PHOTOVIDEO:
                var location = new TextBlock
                {
                    FontSize = 15, Text = $"Место: {(service as PhotoVideoService)!.PhotoLocation}",
                    Margin = margin
                };
                var startTime = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время начала: {(service as PhotoVideoService)!.StartTime:g}",
                    Margin = margin
                };
                var endTime = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время окончания: {(service as PhotoVideoService)!.EndTime:g}",
                    Margin = margin
                };
                listBlock.Add(location);
                listBlock.Add(startTime);
                listBlock.Add(endTime);
                break;
            case Service.ServiceType.RENT:
                var item = new TextBlock
                {
                    FontSize = 15, Text = $"Арендуемый предмет: {(service as RentService)!.RentedItem.Title}",
                    Margin = margin
                };
                var numbers = new TextBlock
                {
                    FontSize = 15, Text = $"Количество: {(service as RentService)!.Number}",
                    Margin = margin
                };
                var startRent = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время начала: {(service as RentService)!.StartTime:g}",
                    Margin = margin
                };
                var endRent = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время окончания: {(service as RentService)!.EndTime:g}",
                    Margin = margin
                };
                listBlock.Add(item);
                listBlock.Add(numbers);
                listBlock.Add(startRent);
                listBlock.Add(endRent);
                break;
            case Service.ServiceType.HALLRENT:
                var hall = new TextBlock
                {
                    FontSize = 15, Text = $"Арендуемое помещение: {(service as HallRentService)!.Hall.Title}",
                    Margin = margin
                };
                startRent = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время начала: {(service as HallRentService)!.StartTime:g}",
                    Margin = margin
                };
                endRent = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время окончания: {(service as HallRentService)!.EndTime:g}",
                    Margin = margin
                };
                listBlock.Add(hall);
                listBlock.Add(startRent);
                listBlock.Add(endRent);
                break;
            case Service.ServiceType.STYLE:
                startTime = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время начала: {(service as HallRentService)!.StartTime:g}",
                    Margin = margin
                };
                endTime = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время окончания: {(service as HallRentService)!.EndTime:g}",
                    Margin = margin
                };
                listBlock.Add(startTime);
                listBlock.Add(endTime);
                break;
        }

        return listBlock;
    }
}