console.log("GenZ Help Loaded Successfully");

/* Mouse Glow Effect */

const glow = document.querySelector(".cursor-glow");

document.addEventListener("mousemove", (e) => {

    glow.style.left = e.clientX + "px";

    glow.style.top = e.clientY + "px";
});


/* Scroll Reveal Animation */

const reveals = document.querySelectorAll(".reveal");

window.addEventListener("scroll", () => {

    reveals.forEach((element) => {

        const windowHeight = window.innerHeight;

        const revealTop = element.getBoundingClientRect().top;

        const revealPoint = 100;

        if(revealTop < windowHeight - revealPoint){

            element.classList.add("active");
        }
    });
});

/* FAQ Accordion */

const faqQuestions = document.querySelectorAll(".faq-question");

faqQuestions.forEach((question) => {

    question.addEventListener("click", () => {

        const answer = question.nextElementSibling;

        if(answer.style.maxHeight){

            answer.style.maxHeight = null;
        }
        else{

            answer.style.maxHeight =
            answer.scrollHeight + "px";
        }
    });
});