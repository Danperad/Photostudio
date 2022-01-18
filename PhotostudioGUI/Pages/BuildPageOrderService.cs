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
        switch (service.Service.ID)
        {
            case 1:
            case 2:
            case 9:
            case 11:
            case 12:
                var location = new TextBlock
                {
                    FontSize = 15, Text = $"Место: {service.PhotoLocation}",
                    Margin = margin
                };
                var startTime = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время начала: {service.StartTime!.Value:g}",
                    Margin = margin
                };
                var endTime = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время окончания: {service.EndTime!.Value:g}",
                    Margin = margin
                };
                listBlock.Add(location);
                listBlock.Add(startTime);
                listBlock.Add(endTime);
                break;
            case 5:
            case 6:
            case 10:
                var item = new TextBlock
                {
                    FontSize = 15, Text = $"Арендуемый предмет: {service.RentedItem!.Title}",
                    Margin = margin
                };
                var numbers = new TextBlock
                {
                    FontSize = 15, Text = $"Количество: {service.Number}",
                    Margin = margin
                };
                var startRent = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время начала: {service.StartTime!.Value:g}",
                    Margin = margin
                };
                var endRent = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время окончания: {service.EndTime!.Value:g}",
                    Margin = margin
                };
                listBlock.Add(item);
                listBlock.Add(numbers);
                listBlock.Add(startRent);
                listBlock.Add(endRent);
                break;
            case 7:
                var hall = new TextBlock
                {
                    FontSize = 15, Text = $"Арендуемое помещение: {service.Hall!.Title}",
                    Margin = margin
                };
                startRent = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время начала: {service.StartTime!.Value:g}",
                    Margin = margin
                };
                endRent = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время окончания: {service.EndTime!.Value:g}",
                    Margin = margin
                };
                listBlock.Add(hall);
                listBlock.Add(startRent);
                listBlock.Add(endRent);
                break;
            case 13:
                startTime = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время начала: {service.StartTime!.Value:g}",
                    Margin = margin
                };
                endTime = new TextBlock
                {
                    FontSize = 15,
                    Text =
                        $"Время окончания: {service.EndTime!.Value:g}",
                    Margin = margin
                };
                listBlock.Add(startTime);
                listBlock.Add(endTime);
                break;
        }

        return listBlock;
    }
}