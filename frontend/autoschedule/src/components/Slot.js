function Slot(lesson, index) {
  
    console.log(lesson.lesson);
    return (
      <td className="lesson-slot">
        <p>{lesson.lesson.subject.name}</p>
        <p>{lesson.lesson.cabinet.number}</p>
        <p>{lesson.lesson.teacher.name}</p>
      </td>
    );
  }
  
  export default Slot;