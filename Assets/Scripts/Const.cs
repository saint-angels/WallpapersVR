using System.Collections;
using System.Collections.Generic;

public enum CollectionType
{
    OLIVIA,
    VALENTINO,
    ALEXANDER,
    WINE,
    MAGNUM,
    FRIENDS,
    SCHOOL
}

public static class Const {
    public static float fadeDuration = 1f;


    public static string DescriptionForCollection (CollectionType collection)
    {
        switch (collection)
        {
            case CollectionType.OLIVIA:
                return "Модный дизайн, который будет актуально и выигрышно смотреться даже в небольшом помещении!";
            case CollectionType.VALENTINO:
                return "Принципиально новый вид фактуры обойного полотна! Богатый дизайн для ценителей классических интерьеров!";
            case CollectionType.ALEXANDER:
                return "Стиль обоев, который гармонично подчеркнет эксклюзивность вашего интерьера и станет изюминкой дизайнерского решения.";
            case CollectionType.WINE:
                return "Атмосфера эксклюзивности, гарантия уюта и гармонии в любом помещении вашего дома!";
            case CollectionType.MAGNUM:
                return "Геометрический дизайн, фактурное покрытие на стене, а также богатая текстильная структура,  придающая интерьеру респектабельный вид!";
            case CollectionType.FRIENDS:
                return "Детишкам понравится дизайн с милыми друзьями, словно из хорошего мультика, а родителям экологичность материала с сохранением всех декоративных и эксплуатационных свойств!";
            case CollectionType.SCHOOL:
                return "Яркие добрые изображения зверей и приятные колористики помогут создать уютную и живую детскую комнату! ";
            default:
                return "-";
        }
    }

    public static string TitleForCollection(CollectionType collection)
    {
        switch (collection)
        {
            case CollectionType.OLIVIA:
                return "OLIVIA";
            case CollectionType.VALENTINO:
                return "VALENTINO";
            case CollectionType.ALEXANDER:
                return "ALEXANDER";
            case CollectionType.WINE:
                return "WINE";
            case CollectionType.MAGNUM:
                return "MAGNUM";
            case CollectionType.FRIENDS:
                return "FRIENDS";
            case CollectionType.SCHOOL:
                return "SCHOOL";
            default:
                return "-";
        }
    }
}
