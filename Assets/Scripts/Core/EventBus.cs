using System;
using System.Collections.Generic;

public class EventBus
{
    // Lưu trữ các sự kiện dưới dạng Delegate chung để có thể chứa bất kỳ Action<T> nào
    private static Dictionary<Type, Delegate> eventDictionary = new Dictionary<Type, Delegate>();

    // Đăng ký sự kiện (Subscribe)
    public static void Subscribe<T>(Action<T> listener)
    {
        Type eventType = typeof(T);
        if (eventDictionary.ContainsKey(eventType))
        {
            // Nếu đã có key, ta gộp thêm listener mới vào delegate hiện tại
            eventDictionary[eventType] = Delegate.Combine(eventDictionary[eventType], listener);
        }
        else
        {
            // Nếu chưa có, ta thêm mới vào dictionary
            eventDictionary.Add(eventType, listener);
        }
    }

    // Hủy đăng ký sự kiện (Unsubscribe)
    public static void Unsubscribe<T>(Action<T> listener)
    {
        Type eventType = typeof(T);
        if (eventDictionary.TryGetValue(eventType, out Delegate existingDelegate))
        {
            // Xóa listener khỏi delegate hiện tại
            Delegate currentDel = Delegate.Remove(existingDelegate, listener);

            if (currentDel == null)
            {
                // Nếu không còn ai lắng nghe nữa thì xóa luôn key khỏi dictionary
                eventDictionary.Remove(eventType);
            }
            else
            {
                // Cập nhật lại delegate sau khi xóa
                eventDictionary[eventType] = currentDel;
            }
        }
    }

    // Phát sự kiện (Publish)
    public static void Publish<T>(T eventData)
    {
        Type eventType = typeof(T);
        if (eventDictionary.TryGetValue(eventType, out Delegate existingDelegate))
        {
            // Ép kiểu Delegate về Action<T> và gọi Invoke truyền dữ liệu vào
            if (existingDelegate is Action<T> callback)
            {
                callback.Invoke(eventData);
            }
        }
    }
}
