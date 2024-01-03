function Slot(lesson, index) {
  
    console.log(lesson.lesson.cabinet.number);
    return (
      <div className="lesson-slot">
        <p>{lesson.lesson.subject.name}</p>
        <p>{lesson.lesson.cabinet.number}</p>
        <p>{lesson.lesson.teacher.name}</p>
      </div>
    );
  }
  
  export default Slot;