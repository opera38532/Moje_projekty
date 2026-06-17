import discord
import random   
from discord.ext import commands, tasks

f_text = ["Agenci konterstrajk, przygotować się!", "Naładowac negevy, wyruszamy!","Igiel typu pisiek"]

intents = discord.Intents.all()
intents.message_content = True
voice_channel_queue: discord.VoiceChannel
text_channel_queue: discord.TextChannel
loop_working = False
current_queue = []
new_queue = []

bot = commands.Bot(command_prefix="^", intents=intents)

async def parse_members(members):
    global current_queue
    global new_queue
    new_queue = current_queue
    in_queue_array = []
    not_queue_array= []

    for current_member in new_queue:
        index = 0
        member_passed = False
        for member in members:
            if current_member == member:
                member_passed = True
        if member_passed == False:
            current_member
                

    for member in members:
        if member.bot == False:
            if member in current_queue:
                in_queue_array.append(member)
            else:
                not_queue_array.append(member)

    print("sorting members complete")
    
    for member in in_queue_array:
        voice = member.voice
        remove_inactive_members(member,voice)

    current_queue = reorder_array(current_queue)

    for member in not_queue_array:
        voice = member.voice
        add_new_active_members(member,voice)

    if new_queue == current_queue:
        print("queue is the same")
        print(new_queue)
        print(current_queue)
    else:
        current_queue = new_queue
        await text_channel_queue.send("nowa kolejka do CS2")
        iterator = 1
        for member in current_queue:
            if member.nick == None:
                text = str(iterator)+". "+str(member.name)
                await text_channel_queue.send(text)
            else:
                text = str(iterator)+". "+str(member.nick)
                await text_channel_queue.send(text)
            iterator += 1
        if len(current_queue) == 5:
            await text_channel_queue.send(str(random.choice(f_text)))


def remove_inactive_members(member,voice):
    global new_queue
    if voice.self_deaf == False and voice.self_mute == False:
        print("user", member.name, "still active") 
    else:
        new_queue.remove(member)
        print("deleted", member.name,"from queue")


def reorder_array(array):
    temp = []
    for i in range(len(array)):
        if array[i] != None:
            temp.append(array[i])
    return temp


def add_new_active_members(member,voice):
    global new_queue
    if voice.self_deaf == False and voice.self_mute == False:
        if len(new_queue) < 5:
            new_queue.append(member)
            

@bot.event
async def on_ready():
    global voice_channel_queue
    global text_channel_queue
    await bot.tree.sync()
    voice_channel_queue = await bot.fetch_channel("1379824179595051050")
    text_channel_queue = await bot.fetch_channel("1381660481768652931")
    print(voice_channel_queue)
    print(text_channel_queue)


@bot.command()
async def odezwijsie(ctx):
    await ctx.send("Kocham piwo")

@bot.command()
async def start(ctx):
    await ctx.send(random.choice(f_text))

@bot.command()
async def VCKick(ctx):
    if ctx.author.nick == None:
        temp = str(ctx.author.name) + " jest nie miły"
    else:
        temp = str(ctx.author.nick) + " jest nie miły"
    await ctx.send(temp)


@bot.command()
async def orwell(ctx):
    global loop_working
    if loop_working == True:
        watcher.cancel()
        loop_working = False
        await ctx.send("Kolejka zakończona")
    else:
        watcher.start()
        loop_working = True
        await ctx.send("Kolejka rozpoczęta")





@tasks.loop(seconds=2.0)
async def watcher():
    global voice_channel_queue
    await parse_members(voice_channel_queue.members)

bot.run('token api discorda')
